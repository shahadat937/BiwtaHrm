using Hrm.Application.DTOs.NavbarThem;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.NavbarThems.Requests.Queries
{
    public class GetNavbarThemByIdRequest : IRequest<NavbarThemDto>
    {
        public int NavbarThemId { get; set; }
    }
}
