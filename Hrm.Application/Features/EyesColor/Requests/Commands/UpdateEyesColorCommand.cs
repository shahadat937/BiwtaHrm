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
    public class UpdateEyesColorCommand : IRequest<BaseCommandResponse>
    {
        public required EyesColorDto EyesColorDto { get; set; }
    }
}
