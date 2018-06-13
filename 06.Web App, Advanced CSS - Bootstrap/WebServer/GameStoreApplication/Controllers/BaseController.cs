namespace WebServer.GameStoreApplication.Controllers
{
    using WebServer.GameStoreApplication.Common;
    using WebServer.GameStoreApplication.Services;
    using WebServer.GameStoreApplication.Services.Contracts;
    using WebServer.Infrastructure;
    using WebServer.Server.Http;
    using WebServer.Server.Http.Contracts;

    public abstract class BaseController : Controller
    {
        // Putt this calass in any project and replace the ApplicationDirectory
        // with the name of the exact project!

        protected const string HomePath = "/";

        private readonly IUserService users; // to check if we are in DB

        protected BaseController(IHttpRequest request)
        {
            this.Request = request;


            this.Authentication = new Authentication(false, false);

            this.users = new UserService();

            this.ApplyAuthentication();
        }


        protected override string ApplicationDirectory => "GameStoreApplication";

          protected IHttpRequest Request { get; private set; }

        // клас пазещ инф-я в проп-та си дали юсерът е логнат и дали е админ!
        protected Authentication Authentication { get; private set; }

        // за да проверяваме дали сме логнатри или не in "/":
        private void ApplyAuthentication()
        {
            // Check for Anonymous Users
            var anonymousDisplay = "flex"; // т.е. да се визуализира само за анонимни
            var authDisplay = "none";   
            var adminDisplay = "none";

            bool isAuthenticated = this.Request // if we are logedin
                .Session
                .Contains(SessionStore.CurrentUserKey);

            // If is Logged in user
            if (isAuthenticated)
            {
                anonymousDisplay = "none";
                authDisplay = "flex";

                var email = this.Request // if we are logedin
                .Session
                .Get<string>(SessionStore.CurrentUserKey);

                // check for Admin user
                var isAdmin = this.users.IsAdmin(email); // authenticatedUserEmail
                if (isAdmin)
                {
                    adminDisplay = "flex";
                }

                // Задаваме че е юсъра е логнат , а дали е админ да вземе от базата:
                this.Authentication = new Authentication(true, isAdmin);
            }

            this.ViewData["anonymousDisplay"] = anonymousDisplay;//none
            this.ViewData["authDisplay"] = authDisplay; // flex
            this.ViewData["adminDisplay"] = adminDisplay;// none
        }

    }
}
