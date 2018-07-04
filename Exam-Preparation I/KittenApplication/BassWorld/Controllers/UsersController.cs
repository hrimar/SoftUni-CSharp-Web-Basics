namespace FDMCats.App.Controllers
{
    using FDMCats.App.Models.BindingModels;
    using FDMCats.Services;
    using FDMCats.Services.Contracts;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Interfaces;
    using SimpleMvc.Framework.Controllers;
    using SimpleMvc.Framework.Security;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using WebServer.Http;

    public class UsersController : BaseController
    {
        private readonly IUserBusinessService userBusinessService;

        public UsersController()
        {
            this.userBusinessService = new UserBusinessService();
        }


        [HttpGet]
        public IActionResult Login()
        {
            this.Model.Data["error"] = string.Empty; // TO HIDE THE ERROR MESSAGE!!!
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel viewModel)
        {
            // 1. Validate attributes:
            if (!this.IsValidModel(viewModel)) // check the attributes of the model
            {
                this.Model.Data["error"] = "Invalid username and/or password";
                return this.View();
            }

            var isUserExistandHasCorrectPassword = this.IsValidModel(viewModel);
            
            // 2. Create new user with Hash password and Add user to DB:
            var loggedUser = this.userBusinessService.Login(viewModel.Username, viewModel.Password);


            // 3. Login the user so session:
            if (isUserExistandHasCorrectPassword && loggedUser)
            {
                this.SignIn(viewModel.Username);
                return this.RedirectToAction("/home/index");
            }
                     
            this.Model.Data["error"] = "Invalid username and/or password";
            return View();

        }

        [HttpGet]
        public IActionResult Register()
        {
            this.Model.Data["error"] = string.Empty; // TO HIDE THE ERROR MESSAGE!!!
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel viewModel)
        {
            // 1. Validation
            var modelIsValid = this.IsValidModel(viewModel);

            // 2. Create new user with Hash password and Add user to DB:
            var registeredUser = this.userBusinessService.Register(
                 viewModel.Username, viewModel.Password, viewModel.ConfirmPassword, viewModel.Email);

            // 3. Login the user so session:
            if (registeredUser)
            {
                this.SignIn(viewModel.Username);
                // this.User = new Authentication(viewModel.Username);
                return this.RedirectToAction("/home/index");
            }

            return View();
        }


        [HttpGet] // !!! 
        public IActionResult Logout()
        {
            this.SignOut();

            return RedirectToAction("/home/index");
        }
    }
}
