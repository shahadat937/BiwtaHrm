using Hrm.Application.DTOs.SelectableOption;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SelectableOption.Requests.Queries
{
    public class GetOptionByIdRequest: IRequest<SelectableOptionDto>
    {
        public int OptionId { get; set; }
    }
}
