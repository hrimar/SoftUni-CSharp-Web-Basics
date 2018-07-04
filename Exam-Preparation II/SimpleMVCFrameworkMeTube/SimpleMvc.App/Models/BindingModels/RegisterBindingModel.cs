
namespace SimpleMvc.App.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterBindingModel
    {
        [Required]
        [MinLength(3)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [MinLength(3)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))] // or [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}
