using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.DTOs.FormSection;
using MediatR;

namespace Hrm.Application.Features.FormSection.Requests.Queries
{
    public class GetFormSectionRequest : IRequest<List<GetFormSectionDto>>
    {

    }
}
