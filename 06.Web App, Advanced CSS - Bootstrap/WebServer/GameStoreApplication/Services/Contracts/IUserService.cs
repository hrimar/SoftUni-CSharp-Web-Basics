using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.GameStoreApplication.Services.Contracts
{
   
    public interface IUserService
    {
        bool Create(string email, string username, string password);

        bool Find(string username, string password);



        bool IsAdmin(string email); // to chech if this person is admin

        //ProfileViewModel Profile(string username);

        //int? GetUserId(string username);



        // bool ExistsByEmail(string email);
    }
}
