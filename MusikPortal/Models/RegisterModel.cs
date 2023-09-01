using System.ComponentModel.DataAnnotations;

namespace MusikPortal.Models
{
    public class RegisterModel
    {
        [Required]
        [Display(Name = "login ")]
        public string? Login { get; set; }

        [Required]
        [Display(Name = "password ")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [Display(Name = "confirm password ")]
        [Compare("Password", ErrorMessage = "passwords do not mutch")]
        [DataType(DataType.Password)]
        public string? PasswordConfirm { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "uncorrectly email ")]
        public string? email { get; set; }
        [Required]
        [Display(Name = "age ")]
        public string? age { get; set; }
    }
}
