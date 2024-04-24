using Hrm.Application.DTOs.HairColor;
using Hrm.Application.DTOs.HairColor;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.HairColor.Requests.Commands
{
    public class UpdateHairColorCommand : IRequest<BaseCommandResponse>
    {
        public required HairColorDto HairColorDto { get; set; }
    }
}
