using DomainLayer.Common.Enums;
using DomainLayer.Entities;
using DomainLayer.Entities.Facets;
using MediatR;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Features.Commands.FacetCommands
{
    public class CreateFacetCommand : IRequest<Guid>
    {
        public string? Name { get; set; }
        public FacetTypeEnum DisplayType { get; set; }
        public bool IsCustom { get; set; }
        public Guid CategoryId { get; set; }

        public List<FacetValueModel>? FacetValues { get; set; }
    }
}
