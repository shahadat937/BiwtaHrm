using Hrm.Application.DTOs.Language;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Language.Requests.Queries
{
    public class GetLanguageByIdRequest : IRequest<LanguageDto>
    {
        public int LanguageId { get; set; }
    }
}
