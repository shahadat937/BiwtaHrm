using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SelectableOption.Requests.Commands
{
    public class DeleteOptionCommand: IRequest<BaseCommandResponse>
    {
        public int OptionId { get; set; }
    }
}
