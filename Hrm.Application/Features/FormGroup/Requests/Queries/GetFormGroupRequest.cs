using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.DTOs.FormGroup;
using MediatR;

namespace Hrm.Application.Features.FormGroup.Requests.Queries
{
    public class GetFormGroupRequest : IRequest<List<FormGroupDto>>
    {

    }
}
