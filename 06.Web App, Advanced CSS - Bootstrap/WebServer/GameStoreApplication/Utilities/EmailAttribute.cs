
using System.ComponentModel.DataAnnotations;

namespace WebServer.GameStoreApplication.Utilities
{
    public class EmailAttribute : ValidationAttribute
    {
        public EmailAttribute()
        {
            this.ErrorMessage = "Email should contain @ sign and a period.";
        }

       
    }
}
