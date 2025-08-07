using AutoMapper;
using DomainLayer.Common.Enums;
using DomainLayer.Entities.Facets;
using DomainLayer.Entities.Products;
using DomainLayer.Entities;
using DomainLayer.Interfaces;
using Microsoft.Extensions.Logging;
using ServiceLayer.Interfaces.FinaInterfaces;
using ServiceLayer.Models.FinaModels.Facets;
using ServiceLayer.Models.FinaModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.FinaServices
{
    public class FinaIntegrationService : IFinaIntegrationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductService _finaProductService;
        private readonly IProductGroupService _finaProductGroupService;
        private readonly ICharacteristicService _finaCharacteristicService;
        private readonly IMapper _mapper;
        private readonly ILogger<FinaIntegrationService> _logger;

        // Cache for mapping FINA IDs to internal GUIDs
        private readonly Dictionary<int, Guid> _categoryMapping = new();
        private readonly Dictionary<int, Guid> _brandMapping = new();
        private readonly Dictionary<int, Guid> _facetMapping = new();
        private readonly Dictionary<string, Guid> _facetValueMapping = new();

        public FinaIntegrationService(
            IUnitOfWork unitOfWork,
            IProductService finaProductService,
            IProductGroupService finaProductGroupService,
            ICharacteristicService finaCharacteristicService,
            IMapper mapper,
            ILogger<FinaIntegrationService> logger)
        {
            _unitOfWork = unitOfWork;
            _finaProductService = finaProductService;
            _finaProductGroupService = finaProductGroupService;
            _finaCharacteristicService = finaCharacteristicService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<SyncResult> SyncAllProductsAsync()
        {
            var result = new SyncResult();

            try
            {
                // First sync groups (categories) and characteristics (facets)
                await SyncProductGroupsAsync();
                await SyncCharacteristicsAsync();

                // Get all FINA data
                var finaProducts = await _finaProductService.GetProductsAsync();
                var finaPrices = await _finaProductService.GetProductPricesAsync();
                var finaCharacteristicValues = await _finaCharacteristicService.GetCharacteristicValuesAsync();

                // Create price lookup
                var priceLookup = finaPrices.ToLookup(p => p.ProductId);
                var characteristicLookup = finaCharacteristicValues.ToLookup(cv => cv.ProductId);

                foreach (var finaProduct in finaProducts)
                {
                    try
                    {
                        var product = await MapAndSaveProductAsync(
                            finaProduct,
                            priceLookup[finaProduct.Id].FirstOrDefault(),
                            characteristicLookup[finaProduct.Id].ToList());

                        result.SuccessCount++;
                        result.ProcessedItems.Add($"Product: {finaProduct.Name} (ID: {finaProduct.Id})");
                    }
                    catch (Exception ex)
                    {
                        result.FailureCount++;
                        result.Errors.Add($"Failed to sync product {finaProduct.Id}: {ex.Message}");
                        _logger.LogError(ex, "Error syncing product {ProductId}", finaProduct.Id);
                    }
                }

                await _unitOfWork.SaveAsync();
                result.Success = result.FailureCount == 0;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Errors.Add($"Critical error during sync: {ex.Message}");
                _logger.LogError(ex, "Critical error during product sync");
            }

            return result;
        }

        public async Task<SyncResult> SyncProductByIdAsync(int finaProductId)
        {
            var result = new SyncResult();

            try
            {
                // Ensure mappings are loaded
                await LoadMappingsAsync();

                var finaProducts = await _finaProductService.GetProductsAsync();
                var finaProduct = finaProducts.FirstOrDefault(p => p.Id == finaProductId);

                if (finaProduct == null)
                {
                    result.Success = false;
                    result.Errors.Add($"Product with ID {finaProductId} not found in FINA");
                    return result;
                }

                var finaPrices = await _finaProductService.GetProductPricesAsync();
                var price = finaPrices.FirstOrDefault(p => p.ProductId == finaProductId);

                var characteristicValues = await _finaCharacteristicService.GetCharacteristicValuesAsync();
                var productCharacteristics = characteristicValues.Where(cv => cv.ProductId == finaProductId).ToList();

                await MapAndSaveProductAsync(finaProduct, price, productCharacteristics);
                await _unitOfWork.SaveAsync();

                result.Success = true;
                result.SuccessCount = 1;
                result.ProcessedItems.Add($"Product: {finaProduct.Name}");
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.FailureCount = 1;
                result.Errors.Add($"Error: {ex.Message}");
                _logger.LogError(ex, "Error syncing product {ProductId}", finaProductId);
            }

            return result;
        }

        public async Task<SyncResult> SyncProductGroupsAsync()
        {
            var result = new SyncResult();

            try
            {
                var finaGroups = await _finaProductGroupService.GetProductGroupsAsync();

                // Sort groups by depth to ensure parents are created before children
                var sortedGroups = finaGroups.OrderBy(g => g.GetDepthLevel());

                foreach (var finaGroup in sortedGroups)
                {
                    try
                    {
                        var category = await MapAndSaveCategoryAsync(finaGroup);
                        _categoryMapping[finaGroup.Id] = category.Id;

                        result.SuccessCount++;
                        result.ProcessedItems.Add($"Category: {finaGroup.Name}");
                    }
                    catch (Exception ex)
                    {
                        result.FailureCount++;
                        result.Errors.Add($"Failed to sync group {finaGroup.Id}: {ex.Message}");
                        _logger.LogError(ex, "Error syncing product group {GroupId}", finaGroup.Id);
                    }
                }

                await _unitOfWork.SaveAsync();
                result.Success = result.FailureCount == 0;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Errors.Add($"Critical error: {ex.Message}");
                _logger.LogError(ex, "Critical error during product group sync");
            }

            return result;
        }

        public async Task<SyncResult> SyncCharacteristicsAsync()
        {
            var result = new SyncResult();

            try
            {
                var finaCharacteristics = await _finaCharacteristicService.GetCharacteristicsAsync();
                var finaCharacteristicValues = await _finaCharacteristicService.GetCharacteristicValuesAsync();

                foreach (var finaChar in finaCharacteristics)
                {
                    try
                    {
                        var facet = await MapAndSaveFacetAsync(finaChar);
                        _facetMapping[finaChar.Id] = facet.Id;

                        // Get unique values for this characteristic
                        var values = finaCharacteristicValues
                            .Where(cv => cv.CharacteristicId == finaChar.Id)
                            .Select(cv => cv.Value)
                            .Distinct()
                            .Where(v => !string.IsNullOrEmpty(v));

                        foreach (var value in values)
                        {
                            var facetValue = await MapAndSaveFacetValueAsync(facet.Id, value!);
                            _facetValueMapping[$"{finaChar.Id}_{value}"] = facetValue.Id;
                        }

                        result.SuccessCount++;
                        result.ProcessedItems.Add($"Characteristic: {finaChar.Name}");
                    }
                    catch (Exception ex)
                    {
                        result.FailureCount++;
                        result.Errors.Add($"Failed to sync characteristic {finaChar.Id}: {ex.Message}");
                        _logger.LogError(ex, "Error syncing characteristic {CharId}", finaChar.Id);
                    }
                }

                await _unitOfWork.SaveAsync();
                result.Success = result.FailureCount == 0;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Errors.Add($"Critical error: {ex.Message}");
                _logger.LogError(ex, "Critical error during characteristics sync");
            }

            return result;
        }

        private async Task<Product> MapAndSaveProductAsync(
            FinaProductModel finaProduct,
            FinaProductPriceModel? price,
            List<FinaProductCharacteristicModel> characteristics)
        {
            // Check if product already exists (using Code as external reference)
            var existingProducts = await _unitOfWork.ProductRepository.GetAllAsync();
            var existingProduct = existingProducts.FirstOrDefault(p =>p.FinaId == finaProduct.Id);

            var product = existingProduct ?? new Product();

            // Map basic product information
            product.Name = finaProduct.Name;
            product.Description = existingProduct?.Description ?? finaProduct.Comment;

            // Map price information
            if (price != null)
            {
                product.Price = price.Price;
                product.DiscountPrice = price.DiscountPrice > 0 ? price.DiscountPrice : null;
            }
            else
            {
                product.Price = 0; // Default price if not provided
            }

            // Map stock status based on minimum quantity
            if (!string.IsNullOrEmpty(finaProduct.MinQuantity) &&
                decimal.TryParse(finaProduct.MinQuantity, out var minQty))
            {
                product.Status = minQty > 0 ? StockStatus.InStock : StockStatus.OutOfStock;
                product.StockAmount = (int)minQty;
            }
            else
            {
                product.Status = StockStatus.OutOfStock;
                product.StockAmount = 0;
            }

            // Default values for other properties
            product.Condition =existingProduct?.Condition ?? Condition.New;
            product.IsActive = existingProduct?.IsActive ?? true;
            product.IsComingSoon = existingProduct?.IsComingSoon ?? false;
            product.IsLiquidated = existingProduct?.IsLiquidated ?? false;
            product.IsNewArrival = existingProduct?.IsNewArrival ?? false;
            product.CreatedDate = existingProduct?.CreatedDate ?? DateTime.UtcNow;

            product.FinaId = finaProduct.Id;
            // Map category
            if (_categoryMapping.TryGetValue(finaProduct.GroupId, out var categoryId))
            {
                var category = await _unitOfWork.CategoryRepository.GetByIdAsync(categoryId);
                product.Category = category;
            }

            // Map brand if exists in additional fields
            var brandField = finaProduct.AdditionalFields?.FirstOrDefault(f =>
                f.Field?.ToLower() == "brand" || f.Field?.ToLower() == "manufacturer");
            if (brandField != null && !string.IsNullOrEmpty(brandField.Value))
            {
                var brand = await GetOrCreateBrandAsync(brandField.Value);
                product.Brand = brand;
            }

            // Map characteristics as facet values
            if (characteristics.Any())
            {
                product.ProductFacetValues = new List<ProductFacetValue>();

                foreach (var charValue in characteristics)
                {
                    var facetValueKey = $"{charValue.CharacteristicId}_{charValue.Value}";
                    if (_facetValueMapping.TryGetValue(facetValueKey, out var facetValueId))
                    {
                        var productFacetValue = new ProductFacetValue
                        {
                            ProductId = product.Id,
                            FacetValueId = facetValueId
                        };
                        product.ProductFacetValues.Add(productFacetValue);
                    }
                }
            }

            // Save product
            if (existingProduct == null)
            {
                await _unitOfWork.ProductRepository.AddAsync(product);
            }
            else
            {
                _unitOfWork.ProductRepository.Update(product);
            }

            return product;
        }

        private async Task<Category> MapAndSaveCategoryAsync(FinaProductGroupModel finaGroup)
        {
            var existingCategories = await _unitOfWork.CategoryRepository.GetAllAsync();
            var existingCategory = existingCategories.FirstOrDefault(c => c.FinaId == finaGroup.Id);

            var category = existingCategory ?? new Category();

            category.Name = finaGroup.Name;
            category.Description = existingCategory?.Description ?? "";
            category.CreatedDate = existingCategory?.CreatedDate ?? DateTime.UtcNow;

            // Map parent category
            if (finaGroup.ParentId.HasValue && finaGroup.ParentId.Value > 0)
            {
                if (_categoryMapping.TryGetValue(finaGroup.ParentId.Value, out var parentId))
                {
                    category.ParentId = parentId;
                }
            }

            if (existingCategory == null)
            {
                await _unitOfWork.CategoryRepository.AddAsync(category);
            }
            else
            {
                _unitOfWork.CategoryRepository.Update(category);
            }

            return category;
        }

        private async Task<Facet> MapAndSaveFacetAsync(FinaCharacteristicModel finaChar)
        {
            var existingFacets = await _unitOfWork.FacetRepository.GetAllAsync();
            var existingFacet = existingFacets.FirstOrDefault(f =>
                f.Name == finaChar.Name);

            var facet = existingFacet ?? new Facet();

            facet.Name = finaChar.Name;
            facet.DisplayType = finaChar.Type == 1
                ? FacetTypeEnum.CheckboxList
                : FacetTypeEnum.SearchBox;
            facet.IsCustom = false;

            if (existingFacet == null)
            {
                await _unitOfWork.FacetRepository.AddAsync(facet);
            }
            else
            {
                _unitOfWork.FacetRepository.Update(facet);
            }

            return facet;
        }

        private async Task<FacetValue> MapAndSaveFacetValueAsync(Guid facetId, string value)
        {
            var existingValues = await _unitOfWork.FacetValueRepository.GetAllAsync();
            var existingValue = existingValues.FirstOrDefault(fv =>
                fv.FacetId == facetId && fv.Value == value);

            if (existingValue != null)
                return existingValue;

            var facetValue = new FacetValue
            {
                FacetId = facetId,
                Value = value,
                ParentId = null
            };

            await _unitOfWork.FacetValueRepository.AddAsync(facetValue);
            return facetValue;
        }

        private async Task<Brand> GetOrCreateBrandAsync(string brandName)
        {
            var existingBrands = await _unitOfWork.BrandRepository.GetAllAsync();
            var existingBrand = existingBrands.FirstOrDefault(b =>
                b.Name != null && b.Name.Equals(brandName, StringComparison.OrdinalIgnoreCase));

            if (existingBrand != null)
                return existingBrand;

            var brand = new Brand
            {
                Name = brandName,
                Description = "Imported from FINA",
                CreateDate = DateTime.UtcNow
            };

            await _unitOfWork.BrandRepository.AddAsync(brand);
            return brand;
        }

        private async Task LoadMappingsAsync()
        {
            // Load existing mappings from database
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
            foreach (var cat in categories.Where(c => c.Description != null && c.Description.Contains("FINA_ID:")))
            {
                var finaId = ExtractFinaId(cat.Description!);
                if (finaId.HasValue)
                    _categoryMapping[finaId.Value] = cat.Id;
            }

            var facets = await _unitOfWork.FacetRepository.GetAllAsync();
            foreach (var facet in facets.Where(f => f.Name != null && f.Name.Contains("FINA_")))
            {
                // Extract FINA ID from name if stored
                // This is a simplified approach - you might want to store FINA IDs differently
            }
        }

        private int? ExtractFinaId(string description)
        {
            var match = System.Text.RegularExpressions.Regex.Match(description, @"FINA_ID:(\d+)");
            if (match.Success && int.TryParse(match.Groups[1].Value, out var id))
                return id;
            return null;
        }
    
    }
}
