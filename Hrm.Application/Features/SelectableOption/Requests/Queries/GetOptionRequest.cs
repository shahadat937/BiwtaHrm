using Hrm.Application.DTOs.SelectableOption;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SelectableOption.Requests.Queries
{
    public class GetOptionRequest: IRequest<List<SelectableOptionDto>>
    {
        
    }
}
