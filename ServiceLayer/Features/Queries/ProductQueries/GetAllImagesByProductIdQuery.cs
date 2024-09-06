using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Features.Queries.ProductQueries
{
    public record GetAllImagesByProductIdQuery(Guid Id) : IRequest<List<string>>;
    
}
