using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebServer.GameStoreApplication.Common;
using WebServer.GameStoreApplication.Utilities;

namespace WebServer.GameStoreApplication.ViewModels.Account
{
    public class RegisterViewModel // for all data comming from register form!!!
    {
        // In order to use anotattions we add method ValidateModel in Controller!!!

        [Display(Name = "E-mail")]
        [Required]
        [MaxLength(ValidationConstants.Account.EmailMaxLength, 
            ErrorMessage = ValidationConstants.InvalidMaxLengthErrorMessage)] // from the class with constatnts
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name ="Full Name")] // -  in order not to write prop. name - "FullName", but my text!!!
        [MinLength(ValidationConstants.Account.NameMinLength,
            ErrorMessage = ValidationConstants.InvalidMinLengthErrorMessage)]
        [MaxLength(ValidationConstants.Account.NameMaxLength,
                 ErrorMessage = ValidationConstants.InvalidMaxLengthErrorMessage)]
        public string FullName { get; set; }

        [Required]
        [MinLength(ValidationConstants.Account.PasswordMinLength,
            ErrorMessage = ValidationConstants.InvalidMinLengthErrorMessage)]
        [MaxLength(ValidationConstants.Account.PasswordMaxLength,
                 ErrorMessage = ValidationConstants.InvalidMaxLengthErrorMessage)]
        [Password]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare(nameof(Password))]  // - to compare the both
        public string ConfirmPassword { get; set; }

    }
}
