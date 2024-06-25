using Hrm.Application.DTOs.Thana;
using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Thana.Requests.Queries
{
    public class GetThanaByUpazilaIdRequest:IRequest<List<SelectedModel>>
    {
        public int UpazilaId { get; set; }
    }
}
