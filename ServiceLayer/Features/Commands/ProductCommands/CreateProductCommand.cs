using DomainLayer.Common.Enums;
using DomainLayer.Entities.Products;
using MediatR;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Features.Commands.ProductCommands
{
    public record CreateProductCommand(string Name,decimal Price,Condition Condition, ICollection<ProductFacetValueModel>? ProductFacetValues, string Description, Guid CategoryId, Guid BrandId) : IRequest<Guid>;

}
