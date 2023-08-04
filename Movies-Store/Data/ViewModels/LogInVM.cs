//using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace Movies_Store.Data.ViewModels
{
    public class LogInVM
    {
        [Required(ErrorMessage = "The email address is required")]
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage ="The password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
