using Amazon.Runtime.Internal;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Features.Commands.FAQCommands;

public record CreateFAQCommand(string Question,string Answer,bool? IsActive,bool? IsFeatured,int? OrderNum) : IRequest<Guid>;
