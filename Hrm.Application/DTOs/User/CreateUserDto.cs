using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hrm.Application.DTOs.User 
{
    public class CreateUserDto
    {
        

       

        [Required(ErrorMessage = "User Name is required")]

        public string UserName { get; set; }


        [Required(ErrorMessage = "Password is required")]

        public string Password { get; set; }


        [Required]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "First Name is required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please.No Space Allow Here")]

        public string FirstName { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please.No Space Allow Here")]
        [Required(ErrorMessage = "Last Name is required")]

        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [RegularExpression(@"(\S)+", ErrorMessage = "White space is not allowed.")]

        public string Email { get; set; }

        [Phone(ErrorMessage = "Invalid Phone Number")]
        [RegularExpression(@"(^([+]{1}[8]{2}|0088)?(01){1}[5-9]{1}\d{8})$", ErrorMessage = "Please enter valid phone number")]
        public string PhoneNumber { get; set; }


        public string? FirstLevel { get; set; }
        public string? SecondLevel { get; set; }
        public string? ThirdLevel { get; set; }
        public string? FourthLevel { get; set; }

        public string RoleName { get; set; }
        public string? TraineeId { get; set; }

    }
}
