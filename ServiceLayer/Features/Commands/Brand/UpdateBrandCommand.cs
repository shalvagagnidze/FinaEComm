using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Features.Commands.Brand
{
    public record UpdateBrandCommand(string Name, string Origin, string Description) : IRequest<Guid>;
}
