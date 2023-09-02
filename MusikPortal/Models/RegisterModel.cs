using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MusikPortal.Models
{
    public class RegisterModel
    {
        [Required]
        [Display(Name = "login ")]
        [Remote("IsLoginInUse", "Login", ErrorMessage = "login is used")]
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
        [Display(Name = "email ")]
        [Remote("IsEmailInUse", "Login", ErrorMessage = "email is used")]
        public string? email { get; set; }
        [Required]
        [Display(Name = "age ")]
        public string? age { get; set; }
    }
}
