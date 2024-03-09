using Hrm.Application.DTOs.Gender;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Gender.Requests.Commands
{
    public class UpdateGenderCommand : IRequest<Unit>
    {
        public GenderDto GenderDto { get; set; }
    }
}
