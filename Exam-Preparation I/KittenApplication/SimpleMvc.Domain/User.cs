﻿namespace SimpleMvc.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {        
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }
            
    }
}
