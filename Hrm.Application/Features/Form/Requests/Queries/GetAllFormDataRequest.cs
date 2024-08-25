using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Form.Requests.Queries
{
    public class GetAllFormDataRequest: IRequest<object>
    {
        public int FormRecordId { get; set; }
    }
}
