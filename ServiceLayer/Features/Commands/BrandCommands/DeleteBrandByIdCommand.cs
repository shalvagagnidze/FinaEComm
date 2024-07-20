using MediatR;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Features.Commands.BrandCommands
{
    public record DeleteBrandByIdCommand(Guid Id) : IRequest;
    
}
