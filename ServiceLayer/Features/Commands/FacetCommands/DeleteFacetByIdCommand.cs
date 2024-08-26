using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Features.Commands.FacetCommands
{
    public record DeleteFacetByIdCommand(Guid id) : IRequest;
}
