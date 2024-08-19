using Hrm.Application.DTOs.SelectableOption;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SelectableOption.Requests.Commands
{
    public class CreateOptionCommand: IRequest<BaseCommandResponse>
    {
        public CreateSelectableOptionDto OptionDto { get; set; }
    }
}
