using Hrm.Application.DTOs.Form;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Form.Requests.Queries
{
    public class GetFormByIdRequest: IRequest<List<FormDto>>
    {
        public int FormId { get; set; }
    }
}
