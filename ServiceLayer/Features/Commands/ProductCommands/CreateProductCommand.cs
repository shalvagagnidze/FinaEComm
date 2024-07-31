using DomainLayer.Common.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Features.Commands.ProductCommands
{
    public record CreateProductCommand(string Name,decimal Price,Gender Gender,ICollection<ProductSize> Size,ICollection<Specification>? Specifications,Condition Condition,string Description, Guid CategoryId, Guid BrandId) : IRequest<Guid>;

}
