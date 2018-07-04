using System;
using System.Collections.Generic;
using System.Text;

namespace FDMCats.Services.Contracts
{
    public interface IUserBusinessService
    {
        bool Validate(string username, string password, string confirmPassword, string email);

        bool Register(string username, string password, string confirmPassword, string email);

        bool Login(string username, string password);
    }
}
