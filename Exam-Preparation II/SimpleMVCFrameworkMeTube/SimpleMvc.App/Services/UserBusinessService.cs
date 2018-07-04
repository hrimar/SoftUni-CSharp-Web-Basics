namespace SimpleMvc.App.Services
{
    using SimpleMvc.App.Services.Contracts;
    using SimpleMvc.Common;
    using SimpleMvc.DataModels;
    using SimpleMvc.Data;
    using System;
    using System.Linq;
    using SimpleMvc.App.Models.ViewModels;
    using Microsoft.EntityFrameworkCore;

    public class UserBusinessService //: IUserBusinessService
    {
        private readonly ExamDbContext dbContex;

        public UserBusinessService()
        {
            this.dbContex = new ExamDbContext();
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
            if (userExisting)
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

        public User GetUserbyUssername(string username)
        {
            var user = this.dbContex.Users.FirstOrDefault(u => u.Username == username);                  

            return user;
        }

        public int GetUsersId(string username)
        {
            var userId = this.dbContex.Users.FirstOrDefault(u => u.Username == username).Id;

            return userId;
        }

        public string GetUsernameById(int uploaderId)
        {
            var username = this.dbContex.Users.FirstOrDefault(u => u.Id == uploaderId).Username;

            return username;
        }

        public string GetUsersEmail(int userId)
        {
            var userEmail = this.dbContex.Users.FirstOrDefault(u => u.Id == userId).Email;

            return userEmail;
        }




        public TubesOfUserViewModel GetTubesOfUser(int userId) // test
        {
            // Ponexe прифиле tr[bva da e  v user/prifileго правим тук:
            var tubesOfUser = this.dbContex.Users
                .Where(u => u.Id == userId)
                .Include(u => u.Tubes)
                .Select(u => new TubesOfUserViewModel
                {
                    UserId = u.Id,
                    TubesProfiles = u.Tubes.Select(t => new TubeProfileViewModel
                    {
                        Title = t.Title,
                        Author = t.Author,
                        TubeId = t.Id
                    }).ToList()
                }).First();
                       
            return tubesOfUser;
        }
    }
}