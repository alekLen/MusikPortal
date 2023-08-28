using System.ComponentModel.DataAnnotations;

namespace MusikPortal.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "введите логин: ")]
        public string? Login { get; set; }

        [Required]
        [Display(Name = "Пароль: ")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
