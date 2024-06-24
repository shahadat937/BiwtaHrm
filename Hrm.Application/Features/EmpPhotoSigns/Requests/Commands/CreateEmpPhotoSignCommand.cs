using Hrm.Application.DTOs.EmpPersonalInfo;
using Hrm.Application.DTOs.EmpPhotoSign;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpPhotoSigns.Requests.Commands
{
    public class CreateEmpPhotoSignCommand : IRequest<BaseCommandResponse>
    {
        public CreateEmpPhotoSignDto EmpPhotoSignDto { get; set; }
    }
}

