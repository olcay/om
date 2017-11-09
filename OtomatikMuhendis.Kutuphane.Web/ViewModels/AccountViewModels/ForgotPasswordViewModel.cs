using System.ComponentModel.DataAnnotations;

namespace OtomatikMuhendis.Kutuphane.Web.ViewModels.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
