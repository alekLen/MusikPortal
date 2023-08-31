using System.ComponentModel.DataAnnotations;

namespace MusikPortal.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "login: ")]
        public string? Login { get; set; }

        [Required]
        [Display(Name = "password: ")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }   
    }
}
