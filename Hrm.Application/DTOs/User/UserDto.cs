using SchoolManagement.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.DTOs.User
{
    public class UserDto
    {


        public string Id { get; set; }
        
        public string UserName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }       

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string RoleName { get; set; }
    
        public bool IsActive { get; set; }

        public int? FirstLevel { get; set; }

        public int? SecondLevel { get; set; }

        public int? ThirdLevel { get; set; }

        public int? FourthLevel { get; set; }

        //get trainee info
        public int? TraineeId { get; set; }
        public string TraineeName { get; set; }



    }
}

