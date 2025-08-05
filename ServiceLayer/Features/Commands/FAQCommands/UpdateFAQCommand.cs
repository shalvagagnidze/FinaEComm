using MediatR;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Features.Commands.FAQCommands;

public record UpdateFAQCommand(FAQModel model) : IRequest<Unit>;
