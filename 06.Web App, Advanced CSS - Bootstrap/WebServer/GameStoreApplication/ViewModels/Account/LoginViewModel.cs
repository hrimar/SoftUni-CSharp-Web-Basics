using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebServer.GameStoreApplication.ViewModels.Account
{
   public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
