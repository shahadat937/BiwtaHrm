using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.User
{
    public class UpdateEmailPhoneDto
    { 
        public string UserId { get; set; }
        public string Email { get; set; }

        
        public string PhoneNumber { get; set; }      

    }
}
 