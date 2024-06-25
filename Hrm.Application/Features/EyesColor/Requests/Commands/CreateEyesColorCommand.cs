using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.EyesColor;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EyesColor.Requests.Commands
{
    public class CreateEyesColorCommand : IRequest<BaseCommandResponse>
    {
        public CreateEyesColorDto EyesColorDto { get; set; }
    }
}
