using SimpleMvc.App.Models.BindingModels;
using SimpleMvc.App.Models.ViewModels;
using SimpleMvc.App.Services;
using SimpleMvc.Framework.Attributes.Methods;
using SimpleMvc.Framework.Attributes.Security;
using SimpleMvc.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SimpleMvc.App.Controllers
{
    public class TubesController : BaseController
    {

        private readonly TubeBusinessService tubeBusinessService; // removed IUserBusinessService
        private readonly UserBusinessService userBusinessService; // removed IUserBusinessService

        public TubesController()
        {
            this.userBusinessService = new UserBusinessService();
            this.tubeBusinessService = new TubeBusinessService();
        }

        [HttpGet]
        [PreAuthorize]
        public IActionResult Upload()
        {
            this.Model.Data["error"] = string.Empty; // TO HIDE THE ERROR MESSAGE!!!
            return View();
        }

        [HttpPost]
        public IActionResult Upload(TubeBindingModel model)
        {
            // 1. Validation
            if (!this.IsValidModel(model)) // check the attributes of the model
            {
                this.Model.Data["error"] = "Invalid username and/or password";
                return this.View();
            }

            // 2. Create new user with Hash password and Add user to DB:
            var userId = this.userBusinessService.GetUsersId(this.User.Name);
            var tube = this.tubeBusinessService.Register(
                 model.Title, model.Author, model.YoutubeLink, model.Description, userId);

            // 3. Login the user so session:
            if (tube)
            {

                return this.RedirectToAction("/home/index"); // ????? WHERE to redirect?
                // may be to details page ?! - try do this
            }

            this.Model.Data["error"] = "Invalid data input";
            return View();
        }

        [HttpGet]
        [PreAuthorize]
        public IActionResult Details(int id) //Оправи ФОРМАТА на страцицата!
        {
            var tube = this.tubeBusinessService.GetTubeById(id);
            tube.Views++;

            // Incorporate the result in the html file:
            var result = new StringBuilder();
            result.AppendLine($"<h1>Run Tish Show</h1>")
                .AppendLine(@"<div class=""container-fluid col - 4"">")
                .AppendLine($"<h1>{tube.Title}</h1>")
                  .AppendLine($@"<iframe width=""560"" height=""315"" src=""https://www.youtube.com/embed/{tube.YoutubeId}"" frameborder=""0"" allowfullscreen> </iframe>")
                        .AppendLine(@"<div class=""col-md-4 col-md-offset-4"">")
                  .AppendLine(@"</div>")
                  .AppendLine($"<div>{tube.Views} Views</div>").AppendLine("<br/>")
                  .AppendLine($"<div>{tube.Description};</div>").AppendLine("<br/>")
                  .AppendLine(@"</div>")
                  .ToString();

            var resultAsHtml = string.Join(Environment.NewLine, result);
            
            this.Model.Data["details"] = resultAsHtml;

            return View();
            //return RedirectToAction("/user/list?page=1")
        }

        [HttpGet]
        [PreAuthorize]
        public IActionResult All()
        {
            this.Model.Data["welcomUser"] = (this.User.Name).ToString();

            var tubes = this.tubeBusinessService.GetAllTubes();

            var result = tubes         //TODO: the picture has to be a link to profile!!!
                  .Select(vm =>
                    $@"<div class=""col-4"">                      
<img src=""https://i.ytimg.com/vi/koLDdZbssSE/hqdefault.jpg?sqp=-oaymwEZCNACELwBSFXyq4qpAwsIARUAAIhCGAFwAQ==&rs=AOn4CLDEA5jeBJdtlnfPUv4kYqQP66BEpg""/>
                        <div> <br/>
                           {vm.Title} <br/>
                            {vm.Author}                            
                        </div>
                    </div>")
                .ToList();                    

            var tubesResult = new StringBuilder();
            tubesResult.Append(@"<div class=""row text-center"">");
            for (int i = 0; i < result.Count; i++)
            {
                tubesResult.Append(result[i]);

                if (i % 3 == 3 - 1)
                {
                    tubesResult.Append(@"</div><div class=""row text-center"">");
                }
            }

            tubesResult.Append("</div>");

            this.Model.Data["tubes"] = tubesResult.ToString();
            return this.View();
        }

        [HttpGet]
        [PreAuthorize]
        public IActionResult Profile() // Mooved to user controller! ok!
        {
            this.Model.Data["username"] = (this.User.Name).ToString();

            var userId = this.userBusinessService.GetUsersId(this.User.Name);
            var userEmail = this.userBusinessService.GetUsersEmail(userId);

            this.Model.Data["email"] = userEmail;
            
            // Get Тубе from db:
            var tubsOfUser = this.tubeBusinessService.GetTubesOfUser(userId);
            // Now the received data from GameService has to be generate in html file with buttons:
            var result = tubsOfUser
                 .Select(g => new StringBuilder()
                    .AppendLine("<tr>")
                        .AppendLine($"<td>{g.TubeId}</td>")
                        .AppendLine($"<td>{g.Title}</td>")
                        .AppendLine($"<td>{g.Author:F1} GB</td>")                     
                        .AppendLine($"<td>")
                            .AppendLine($@"<a  href=""/tubes/details/{g.TubeId}"">Details</a>")
                        .AppendLine($"</td>")
                    .AppendLine("</tr>")
                    .ToString());

            var resultAsHtml = string.Join(Environment.NewLine, result);

            this.Model.Data["result"] = resultAsHtml; // replate this with the other table!
            
            return this.View();
        }

    }
}
