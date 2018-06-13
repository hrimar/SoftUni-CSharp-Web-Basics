using System;
using System.Collections.Generic;
using System.Linq;
using WebServer.GameStoreApplication.Data;
using WebServer.GameStoreApplication.Data.Models;
using WebServer.GameStoreApplication.Services.Contracts;

namespace WebServer.GameStoreApplication.Services
{
    public class UserService : IUserService
    {

        public bool Create(string email, string name, string password)
        {
            using (var db = new GameStoreDbContext())
            {
                if (db.Users.Any(u=>u.Email == email))
                {
                    return false;
                }

                var isAdmin = !db.Users.Any(); // the first user is begome an admin

                var user = new User
                {
                    Email = email,
                    Name = name,
                    Password = password,
                    IsAdmin = isAdmin
                };

                db.Users.Add(user);
                db.SaveChanges();
            }

            return true;
        }

        public bool Find(string email, string password)
        {
            using (var db = new GameStoreDbContext())
            {
                return db.Users.Any(u => u.Email == email && u.Password == password);
            }
        }

        public bool IsAdmin(string email)
        {
            using (var db = new GameStoreDbContext())
            {
                return db.Users.Any(u => u.Email == email && u.IsAdmin);
            }
            
        }


    }
}
