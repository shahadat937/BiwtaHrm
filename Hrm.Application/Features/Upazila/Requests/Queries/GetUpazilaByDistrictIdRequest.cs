using Hrm.Application.DTOs.Upazila;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Upazila.Requests.Queries
{
    public class GetUpazilaByDistrictIdRequest:IRequest<List<UpazilaDto>>
    {
        public int DistrictId { get; set; }
    }
}
