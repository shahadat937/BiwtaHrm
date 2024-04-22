using Hrm.Application.DTOs.EyesColor;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EyesColor.Requests.Commands
{
    public class UpdateEyesColorCommand : IRequest<Unit>
    {
        public EyesColorDto EyesColorDto { get; set; }
    }
}
