﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Models.Identity
{
    public class VerifyTokenRequest
    {
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}
