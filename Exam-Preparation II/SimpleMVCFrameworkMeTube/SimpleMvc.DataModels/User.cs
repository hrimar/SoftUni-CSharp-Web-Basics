namespace SimpleMvc.DataModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        //////[Key]
        //////[RegularExpression(@"^([A-Za-z]){7}\d{3}$")]
        //////public string SerialNumber { get; set; }
        //////[RegularExpression(@"^(\+359|0)\d{9}$")]
        //////public string OwnerPhoneNumber { get; set; }

        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public ICollection<Tube> Tubes { get; set; }

    }
}
