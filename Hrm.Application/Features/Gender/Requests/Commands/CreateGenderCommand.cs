using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.Gender;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Gender.Requests.Commands
{
    public class CreateGenderCommand : IRequest<BaseCommandResponse>
    {
        public CreateGenderDto GenderDto { get; set; }
    }
}
