using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Form.Requests.Commands
{
    public class CreateFormDataCommand: IRequest<BaseCommandResponse>
    {
        public object formData { get; set; }
    }
}
