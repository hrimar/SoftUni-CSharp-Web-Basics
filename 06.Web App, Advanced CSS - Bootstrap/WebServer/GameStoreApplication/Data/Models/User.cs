using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebServer.GameStoreApplication.Common;

namespace WebServer.GameStoreApplication.Data.Models
{
    public class User
    {
        public int Id { get; set; } // (in EF Id is string)

        [MinLength(ValidationConstants.Account.NameMinLength)]
        [MaxLength(ValidationConstants.Account.NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(ValidationConstants.Account.EmailMaxLength)] // from the class with constatnts
        //[DataType(DataType.EmailAddress)]
        public string Email { get; set; }
            
        [Required]
        [MinLength(ValidationConstants.Account.PasswordMinLength)]
        [MaxLength(ValidationConstants.Account.PasswordMaxLength)]
        public string Password { get; set; }
       
        public bool IsAdmin { get; set; }


        public ICollection<UserGame> Games { get; set; } = new List<UserGame>();
    }
}
