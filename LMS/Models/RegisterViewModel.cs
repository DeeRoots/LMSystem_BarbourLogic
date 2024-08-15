using System.ComponentModel.DataAnnotations;

namespace LMS.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

                
        //public bool IsMember {  get; set; }

        
        //public bool IsEmployee { get; set; }

        
        //public bool IsAdmin { get; set; }

    }
}
