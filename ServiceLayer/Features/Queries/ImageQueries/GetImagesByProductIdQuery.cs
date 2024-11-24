using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Features.Queries.ImageQueries
{
    public record GetImagesByProductIdQuery(Guid id) : IRequest<ActionResult>;

}
