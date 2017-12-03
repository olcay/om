using System.ComponentModel.DataAnnotations;

namespace OtomatikMuhendis.Kutuphane.Web.Core.ViewModels.AccountViewModels
{
    public class LoginWithRecoveryCodeViewModel
    {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Recovery Code")]
            public string RecoveryCode { get; set; }
    }
}
