using System.ComponentModel.DataAnnotations;

namespace OtomatikMuhendis.Kutuphane.Web.Core.ViewModels.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
