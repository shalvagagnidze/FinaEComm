using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces.FinaInterfaces;
using ServiceLayer.Services.FinaServices;

namespace ECommerceOutdoor.Controllers.FinaControllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class FinaSyncController : ControllerBase
    {
        private readonly IFinaIntegrationService _integrationService;
        private readonly TokenStorageService _tokenStorage;

        public FinaSyncController(
            IFinaIntegrationService integrationService,
            TokenStorageService tokenStorage)
        {
            _integrationService = integrationService;
            _tokenStorage = tokenStorage;
        }

        /// <summary>
        /// Synchronizes all products from FINA ERP to the local database
        /// </summary>
        [HttpPost("sync-all")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> SyncAllProducts()
        {
            try
            {
                // Check if token is valid
                if (!_tokenStorage.IsTokenValid())
                {
                    return Unauthorized(new
                    {
                        message = "FINA authentication token is invalid or expired. Please authenticate first.",
                        ex = "Token validation failed"
                    });
                }

                var result = await _integrationService.SyncAllProductsAsync();

                if (result.Success)
                {
                    return Ok(new
                    {
                        message = "Synchronization completed successfully",
                        successCount = result.SuccessCount,
                        failureCount = result.FailureCount,
                        processedItems = result.ProcessedItems,
                        errors = result.Errors
                    });
                }
                else
                {
                    return StatusCode(500, new
                    {
                        message = "Synchronization completed with errors",
                        successCount = result.SuccessCount,
                        failureCount = result.FailureCount,
                        processedItems = result.ProcessedItems,
                        errors = result.Errors
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Synchronization failed",
                    error = ex.Message,
                    ex = ex.ToString()
                });
            }
        }

        /// <summary>
        /// Synchronizes a single product by FINA ID
        /// </summary>
        [HttpPost("sync-product/{finaProductId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> SyncProductById(int finaProductId)
        {
            try
            {
                if (!_tokenStorage.IsTokenValid())
                {
                    return Unauthorized(new
                    {
                        message = "FINA authentication token is invalid or expired",
                        ex = "Token validation failed"
                    });
                }

                var result = await _integrationService.SyncProductByIdAsync(finaProductId);

                if (result.Success)
                {
                    return Ok(new
                    {
                        message = $"Product {finaProductId} synchronized successfully",
                        processedItems = result.ProcessedItems
                    });
                }
                else if (result.Errors.Any(e => e.Contains("not found")))
                {
                    return NotFound(new
                    {
                        message = $"Product {finaProductId} not found in FINA",
                        errors = result.Errors
                    });
                }
                else
                {
                    return StatusCode(500, new
                    {
                        message = $"Failed to synchronize product {finaProductId}",
                        errors = result.Errors
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Synchronization failed",
                    error = ex.Message,
                    ex = ex.ToString()
                });
            }
        }

        /// <summary>
        /// Synchronizes product groups (categories) from FINA
        /// </summary>
        [HttpPost("sync-groups")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> SyncProductGroups()
        {
            try
            {
                if (!_tokenStorage.IsTokenValid())
                {
                    return Unauthorized(new
                    {
                        message = "FINA authentication token is invalid or expired",
                        ex = "Token validation failed"
                    });
                }

                var result = await _integrationService.SyncProductGroupsAsync();

                if (result.Success)
                {
                    return Ok(new
                    {
                        message = "Product groups synchronized successfully",
                        successCount = result.SuccessCount,
                        failureCount = result.FailureCount,
                        processedItems = result.ProcessedItems,
                        errors = result.Errors
                    });
                }
                else
                {
                    return StatusCode(500, new
                    {
                        message = "Product groups synchronization completed with errors",
                        successCount = result.SuccessCount,
                        failureCount = result.FailureCount,
                        processedItems = result.ProcessedItems,
                        errors = result.Errors
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Synchronization failed",
                    error = ex.Message,
                    ex = ex.ToString()
                });
            }
        }

        /// <summary>
        /// Synchronizes characteristics (facets) from FINA
        /// </summary>
        [HttpPost("sync-characteristics")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> SyncCharacteristics()
        {
            try
            {
                if (!_tokenStorage.IsTokenValid())
                {
                    return Unauthorized(new
                    {
                        message = "FINA authentication token is invalid or expired",
                        ex = "Token validation failed"
                    });
                }

                var result = await _integrationService.SyncCharacteristicsAsync();

                if (result.Success)
                {
                    return Ok(new
                    {
                        message = "Characteristics synchronized successfully",
                        successCount = result.SuccessCount,
                        failureCount = result.FailureCount,
                        processedItems = result.ProcessedItems,
                        errors = result.Errors
                    });
                }
                else
                {
                    return StatusCode(500, new
                    {
                        message = "Characteristics synchronization completed with errors",
                        successCount = result.SuccessCount,
                        failureCount = result.FailureCount,
                        processedItems = result.ProcessedItems,
                        errors = result.Errors
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Synchronization failed",
                    error = ex.Message,
                    ex = ex.ToString()
                });
            }
        }

        /// <summary>
        /// Performs a full synchronization in the correct order
        /// </summary>
        [HttpPost("full-sync")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> FullSync()
        {
            try
            {
                if (!_tokenStorage.IsTokenValid())
                {
                    return Unauthorized(new
                    {
                        message = "FINA authentication token is invalid or expired",
                        ex = "Token validation failed"
                    });
                }

                var results = new
                {
                    groups = await _integrationService.SyncProductGroupsAsync(),
                    characteristics = await _integrationService.SyncCharacteristicsAsync(),
                    products = await _integrationService.SyncAllProductsAsync()
                };

                var totalSuccess = results.groups.SuccessCount +
                                 results.characteristics.SuccessCount +
                                 results.products.SuccessCount;

                var totalFailure = results.groups.FailureCount +
                                 results.characteristics.FailureCount +
                                 results.products.FailureCount;

                var allErrors = new List<string>();
                allErrors.AddRange(results.groups.Errors);
                allErrors.AddRange(results.characteristics.Errors);
                allErrors.AddRange(results.products.Errors);

                var overallSuccess = results.groups.Success &&
                                    results.characteristics.Success &&
                                    results.products.Success;

                if (overallSuccess)
                {
                    return Ok(new
                    {
                        message = "Full synchronization completed successfully",
                        summary = new
                        {
                            totalSuccessCount = totalSuccess,
                            totalFailureCount = totalFailure,
                            groupsProcessed = results.groups.SuccessCount,
                            characteristicsProcessed = results.characteristics.SuccessCount,
                            productsProcessed = results.products.SuccessCount
                        },
                        details = results,
                        errors = allErrors
                    });
                }
                else
                {
                    return StatusCode(500, new
                    {
                        message = "Full synchronization completed with errors",
                        summary = new
                        {
                            totalSuccessCount = totalSuccess,
                            totalFailureCount = totalFailure,
                            groupsProcessed = results.groups.SuccessCount,
                            groupsFailed = results.groups.FailureCount,
                            characteristicsProcessed = results.characteristics.SuccessCount,
                            characteristicsFailed = results.characteristics.FailureCount,
                            productsProcessed = results.products.SuccessCount,
                            productsFailed = results.products.FailureCount
                        },
                        details = results,
                        errors = allErrors
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Full synchronization failed",
                    error = ex.Message,
                    ex = ex.ToString()
                });
            }
        }

        /// <summary>
        /// Gets the current synchronization status
        /// </summary>
        [HttpGet("status")]
        [ProducesResponseType(200)]
        public IActionResult GetSyncStatus()
        {
            return Ok(new
            {
                tokenValid = _tokenStorage.IsTokenValid(),
                message = _tokenStorage.IsTokenValid()
                    ? "Ready to synchronize"
                    : "Authentication required before synchronization"
            });
        }
    }
}
