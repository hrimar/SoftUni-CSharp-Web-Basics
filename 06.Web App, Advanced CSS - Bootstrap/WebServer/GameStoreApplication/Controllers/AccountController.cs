using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WebServer.GameStoreApplication.Services;
using WebServer.GameStoreApplication.Services.Contracts;
using WebServer.GameStoreApplication.ViewModels.Account;
using WebServer.Server.Http;
using WebServer.Server.Http.Contracts;
using WebServer.Server.Http.Response;

namespace WebServer.GameStoreApplication.Controllers
{
   public class AccountController : BaseController
    {
        private const string RegisterView = @"account\register";
        private const string LoginView = @"account\login";
        private const string ProfileView = @"account\profile";

        private IUserService userService;

        public AccountController(IHttpRequest request)//(IUserService userService)
            : base(request)
        {
            //this.userService = userService;
            this.userService = new UserService();
        }

        public IHttpResponse Register()
        {
          //  this.SetDefaultView(); // hide/show logout button. But we replace this logic with this in BaseController

            return this.FileViewResponse(@"account\register");
        }        

        private void SetDefaultView()// hide/show logout button. But we replace this logic with this in BaseController
        {
            this.ViewData["anonymousDisplay"] = "block";
              this.ViewData["authDisplay"] = "none"; // hide logout button
        }
          


        public IHttpResponse Register( RegisterViewModel viewModel) // remove: IHttpRequest req, because include it in BaseController
        {
           // this.SetDefaultView(); // hide/show logout button

            if (!this.ValidateModel(viewModel))
            {                
                //return this.FileViewResponse(@"account\register"); // or 
                return this.Register();
                // т.е ако не е валиден върни същото
            }

            // After creating UserServices:
            var success = this.userService.Create(viewModel.Email, viewModel.FullName, viewModel.Password);
            if (!success)
            {
                this.ShowError("E-mail is taken.");

                return this.Register();
            }
            else
            {
                this.Request.Session.Add(SessionStore.CurrentUserKey, viewModel.Email); // unique user key!

                return new RedirectResponse("/");
            }

        }
        //public IHttpResponse Register(RegisterViewModel viewModel)
        //{
        //    if (viewModel != null)
        //    {
        //        bool isValid = this.ValidateRegisterViewModel(viewModel);
        //        if (!isValid)
        //        {                    
        //            return new RedirectResponse("/register");
        //        }
        //    }

        //    return null;
        //}

        //private bool ValidateRegisterViewModel(RegisterViewModel viewModel)
        //{
        //    var userEmail = viewModel.Email; // comes from the form via RegisterViewModel
        //    // o	Email – must contain @ sign and a period. 
        //    if (string.IsNullOrEmpty(userEmail))
        //    {               

        //        return false;
        //    }

        //    var userExistsByEmail = this.userService.ExistsByEmail(userEmail);
        //    if (userExistsByEmail)
        //    {
        //        return false;
        //    }

        //    var isValidEmail = userEmail.Contains("@") && userEmail.Contains(".");
        //    if (!isValidEmail)
        //    {
        //        return false;
        //    }


        //    // o	Password – length must be at least 6 symbols and must contain at least 1 uppercase, 1 lowercase letter and 1 digit
        //    var userPassword = viewModel.Password;
        //    var userConfirmPassword = viewModel.ConfirmPassword;

        //    if (string.IsNullOrEmpty(userPassword) || string.IsNullOrEmpty(userConfirmPassword)
        //        || userPassword != userConfirmPassword)
        //    {
        //        return false;
        //    }

        //    var isValidPassword = userPassword.Any(up =>
        //    Char.IsNumber(up) &&
        //    Char.IsLower(up) &&
        //    Char.IsUpper(up));
        //    if (!isValidPassword)
        //    {
        //        return false;
        //    }

        //    return true; 
        //}


        public IHttpResponse Login()
        {
           // this.SetDefaultView(); // hide/show logout button

            return this.FileViewResponse(@"account\login");
        }

        public IHttpResponse Login( LoginViewModel viewModel) //   IHttpRequest req,
        {

            if (!this.ValidateModel(viewModel))
            {
                return this.Login();
            }

            var success = this.userService.Find(viewModel.Email, viewModel.Password);
            if (!success)
            {
                this.ShowError("Invalid user details.");

                return this.FileViewResponse(LoginView); // or return this.Login();
            }
            else
            {
                this.Request.Session.Add(SessionStore.CurrentUserKey, viewModel.Email); // unique user key!

                return new RedirectResponse("/");
            }
        }

        public IHttpResponse Logout()
        {
            this.Request.Session.Clear();

            return this.RedirectResponse("/");
        }
        

    }
    
}
