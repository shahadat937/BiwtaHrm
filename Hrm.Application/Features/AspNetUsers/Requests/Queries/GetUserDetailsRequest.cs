using Hrm.Application.DTOs.AspNetUsers;
using Hrm.Application.DTOs.BloodGroup;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.AspNetUsers.Requests.Queries
{
    public class GetUserDetailsRequest : IRequest<object>
    {
        [Required]
        public string Id { get; set; }
    }
}
