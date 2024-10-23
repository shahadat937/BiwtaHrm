using Hrm.Application.DTOs.Form;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Form.Requests.Commands
{
    public class UpdateFormDataCommand: IRequest<BaseCommandResponse>
    {
        public FormDataDto formData {  get; set; }
        public int UpdateRole {  get; set; }
    }
}
