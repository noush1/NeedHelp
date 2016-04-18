using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace needHelp.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "הסיסמה {0} חייבת להכיל לפחות {2} תווים.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "סיסמה")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "אימות סיסמה")]
        [Compare("NewPassword", ErrorMessage = "סיסמה ואימות הסיסמה אינם זהים.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "סיסמה קודמת")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "הסיסמה {0} חייבת להכיל לפחות {2} תווים.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "סיסמה חדשה")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "אימות סיסמה")]
        [Compare("NewPassword", ErrorMessage = "סיסמה ואימות הסיסמה אינם זהים.")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "טלפון")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "קוד")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "טלפון")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}