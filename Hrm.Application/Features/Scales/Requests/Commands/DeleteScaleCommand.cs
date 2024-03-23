using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Scales.Requests.Commands
{
    public class DeleteScaleCommand : IRequest
    {
        public int ScaleId { get; set; }
    }
}
