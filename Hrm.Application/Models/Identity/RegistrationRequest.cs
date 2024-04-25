using System.ComponentModel.DataAnnotations;

namespace Hrm.Application.Models.Identity
{
    public class RegistrationRequest
    {
        [Required] 
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string UserName { get; set; }
        
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string PNo { get; set; }


        [Required]
        public bool IsActive { get; set; } = true;

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
