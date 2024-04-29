using Hrm.Application.DTOs.District;
using Hrm.Application.DTOs.Division;
using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.District.Requests.Queries
{
    public class GetDistrictByDivisionIdRequest : IRequest<List<SelectedModel>>
    {
        public int DivisionId { get; set; }
    }
}
