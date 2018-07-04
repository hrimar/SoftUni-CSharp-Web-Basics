namespace SimpleMvc.App.Controllers
{
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Interfaces;
    using SimpleMvc.Framework.Controllers;

    public class HomeController : BaseController
    {

        //[HttpGet]
        //public IActionResult Index()
        //{
        //    if(this.User.IsAuthenticated)
        //    {
        //        this.Model.Data["username"] = this.User.Name;
        //    }
        //   return View();  
        //}

        [HttpGet]
        public IActionResult Index()
        {
            if (this.User.IsAuthenticated)
            {
                this.Model.Data["homeContent"] =
                     $@"<div class=""jumbotron"" >
                <p class=""h1 display-3"">Welcome, {this.User.Name}</p>
                       
                </div>";
            }
            else
            {
                this.Model.Data["homeContent"] =
                   $@"<div class=""jumbotron"" >
    <p class=""h1 display-3"">Welcome to MeTube&trade;!</p>
    <p class=""h3"">The simplest, easiest to use, most comfortable Multimedia Application.</p>
    <hr class=""my-3"">
    <p><a href=""/users/login"">Login</a> if you have an account or <a href=""/users/register"">Register</a> now and start tubing.</p>
    </div>";
            }
            return View();
        }
    }
}
