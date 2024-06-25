using Hrm.Application.DTOs.Language;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Language.Requests.Commands
{
    public class UpdateLanguageCommand : IRequest<BaseCommandResponse>
    {
        public LanguageDto LanguageDto { get; set; }
    }
}
