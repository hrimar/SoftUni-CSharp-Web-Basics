using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.GameStoreApplication.Common
{
    // клас пазещ инф-я дали усера е логнат и дали е админ!
    // Инф-ят аидва от BaseController, където се чеква и сетват тези проп-та!

  public  class Authentication
    {
        public Authentication(bool isAuthenticated, bool isAdmin)
        {
            this.IsAuthenticated = isAuthenticated;
            this.IsAdmin = isAdmin;
        }

        public bool IsAuthenticated { get; private set; }

        public bool IsAdmin { get; private set; }
    }
}
