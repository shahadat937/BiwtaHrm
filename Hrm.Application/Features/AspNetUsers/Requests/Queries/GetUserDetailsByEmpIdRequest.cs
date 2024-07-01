﻿using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.AspNetUsers.Requests.Queries
{
    public class GetUserDetailsByEmpIdRequest : IRequest<object>
    {
        public int Id { get; set; }
    }
}