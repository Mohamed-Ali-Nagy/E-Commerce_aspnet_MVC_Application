//using Microsoft.Build.Framework;

using System.ComponentModel.DataAnnotations;

namespace Movies_Store.Data.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage ="The name is required")]
        [Display(Name ="Full name")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "The usrname is required")]
        [Display(Name = "username")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "The email address is required")]
        [Display(Name = "Email address")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "The password required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "The confirm required")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Passwords do not matchs")]
        public string ConfirmPassword { get; set; }
    }
}
