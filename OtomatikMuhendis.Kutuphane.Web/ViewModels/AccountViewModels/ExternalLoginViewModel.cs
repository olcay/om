using System.ComponentModel.DataAnnotations;

namespace OtomatikMuhendis.Kutuphane.Web.ViewModels.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
