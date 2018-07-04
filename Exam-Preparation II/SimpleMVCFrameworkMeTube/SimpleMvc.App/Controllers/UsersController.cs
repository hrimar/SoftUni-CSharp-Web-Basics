namespace SimpleMvc.App.Controllers
{
    using SimpleMvc.App.Models.BindingModels;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Interfaces;
    using SimpleMvc.App.Services.Contracts;
    using SimpleMvc.App.Services;
    using System;
    using System.Text;
    using SimpleMvc.Framework.Attributes.Security;
    using System.Linq;
    using System.IO;

    public class UsersController : BaseController
    {
        private readonly UserBusinessService userBusinessService; // removed IUserBusinessService

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
        public IActionResult Login(LoginBindingModel model)
        {
            // 1. Validate attributes:
            if (!this.IsValidModel(model)) // check the attributes of the model
            {
                this.Model.Data["error"] = "Invalid username and/or password";
                return this.View();
            }

            var isUserExistandHasCorrectPassword = this.IsValidModel(model);
            
            // 2. Create new user with Hash password and Add user to DB:
            var loggedUser = this.userBusinessService.Login(model.Username, model.Password);


            // 3. Login the user to session:
            if (isUserExistandHasCorrectPassword && loggedUser)
            {
                this.SignIn(model.Username);
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
        public IActionResult Register(RegisterBindingModel model)
        {
            // 1. Validation
            if (!this.IsValidModel(model)) // check the attributes of the model
            {
                this.Model.Data["error"] = "Invalid username and/or password";
                return this.View();
            }

            // 2. Create new user with Hash password and Add user to DB:
            var registeredUser = this.userBusinessService.Register(
                 model.Username, model.Password, model.ConfirmPassword, model.Email);

            // 3. Login the user so session:
            if (registeredUser)
            {
                this.SignIn(model.Username);
                // this.User = new Authentication(viewModel.Username);
                return this.RedirectToAction("/home/index");
            }

            this.Model.Data["error"] = "Invalid data input";
            return View();
        }


        [HttpGet] // !!! 
        public IActionResult Logout()
        {
            this.SignOut();

            return RedirectToAction("/home/index");
        }



        [HttpGet]
        [PreAuthorize]
        public IActionResult Profile() // OK
        {
            this.Model.Data["username"] = (this.User.Name).ToString();

            var userId = this.userBusinessService.GetUsersId(this.User.Name);
            var userEmail = this.userBusinessService.GetUsersEmail(userId);

            this.Model.Data["email"] = userEmail;

            // Get Тубе from db:
            var tubsOfUser = this.userBusinessService.GetTubesOfUser(userId);
            // Now the received data from GameService has to be generate in html file with buttons:
            var result = tubsOfUser.TubesProfiles
                 .Select(g => new StringBuilder()
                    .AppendLine("<tr>")
                        .AppendLine($"<td>{g.TubeId}</td>")
                        .AppendLine($"<td>{g.Title}</td>")
                        .AppendLine($"<td>{g.Author:F1} GB</td>")
                        .AppendLine($"<td>")
                            .AppendLine($@"<a  href=""/tubes/details?id={g.TubeId}"">Details</a>")
                        .AppendLine($"</td>")
                    .AppendLine("</tr>")
                    .ToString());

            var resultAsHtml = string.Join(Environment.NewLine, result);

            this.Model.Data["result"] = resultAsHtml; // replate this with the other table!
            
            return this.View();
        }

        //[HttpGet]      
        //public IActionResult Test() // Опит за четене на HTML_a от html file, не писан тук.
        //{
        //    string title = "To read from file";
        //    int views = 100;
        //    string description = "Description to read from file";

        //    // Just for test to read from HTML file the above html:
        //    var resultFormat = File.ReadAllText("../../../Views/Users/test.html");
        //    var resultAsHtml = string.Format(resultFormat, title, views, description);

        //    return this.View();
        //}
    }
}
