﻿using System.ComponentModel.DataAnnotations;

namespace Otomatik.Library.Web.Core.ViewModels.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
