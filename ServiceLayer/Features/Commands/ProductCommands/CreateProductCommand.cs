using DomainLayer.Common.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Features.Commands.ProductCommands
{
    public record CreateProductCommand(string Name,decimal Price,Sex Sex,ProductSize Size,Condition Condition,string Description, Guid CategoryId, Guid BrandId) : IRequest<Guid>;

}
