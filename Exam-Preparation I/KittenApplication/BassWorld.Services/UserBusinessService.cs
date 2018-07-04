namespace FDMCats.Services
{
    using FDMCats.Services.Contracts;
    using SimpleMvc.Common;
    using SimpleMvc.Data;
    using SimpleMvc.Domain;
    using System;
    using System.Linq;

    public class UserBusinessService : IUserBusinessService
    {
        private readonly KittenDbContext dbContex;

        public UserBusinessService()
        {
            this.dbContex = new KittenDbContext();
        }

        public bool Validate(string username, string password, string confirmPassword, string email)
        {
            var passwordMatch = password == confirmPassword;
            if (!passwordMatch)
            {
                return false;
            }

            var userExisting = this.dbContex.Users.Any(u => u.Username == username ||
               u.Email == email);
            if (userExisting)//my
            {
                return false;
            }

            return true;
        }

        public bool Register(string username, string password, string confirmPassword, string email)
        {
            var isNewUserValid = this.Validate(username, password, confirmPassword, email);
            if (isNewUserValid)
            {
                var newUser = new User
                {
                    Username = username,
                    PasswordHash = PasswordUtilities.GetPasswordHash(password),
                    Email = email
                };


                using (this.dbContex)
                {
                    this.dbContex.Users.Add(newUser);
                    this.dbContex.SaveChanges();
                }


                return true;
            }

            return false;
        }

        public bool Login(string username, string password)
        {
            var user = this.dbContex.Users.FirstOrDefault(u => u.Username == username);

            if (user != null && user.PasswordHash == PasswordUtilities.GetPasswordHash(password))
            {
                return true;
            }

            return false;
        }
    }
}
