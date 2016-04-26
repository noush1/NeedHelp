using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace needHelp.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "אימייל")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "קוד")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "שמור בדפדפן")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "אימייל")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "אימייל")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "סיסמה")]
        public string Password { get; set; }

        [Display(Name = "זכור אותי")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "שם פרטי")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "שם משפחה")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "אימייל")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "הסיסמה {0} חייבת להכיל לפחות {2} תווים.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "סיסמה")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "אימות סיסמה")]
        [Compare("Password", ErrorMessage = "סיסמה ואימות הסיסמה אינם זהים.")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "איש קשר")]
        public string ContactName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "אתר")]
        public string Website { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "טלפון")]
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "סוג")]
        public string Type { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "אימייל")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "הסיסמה {0} חייבת להכיל לפחות {2} תווים.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "סיסמה")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "אימות סיסמה")]
        [Compare("Password", ErrorMessage = "סיסמה ואימות הסיסמה אינם זהים.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "אימייל")]
        public string Email { get; set; }
    }
}
