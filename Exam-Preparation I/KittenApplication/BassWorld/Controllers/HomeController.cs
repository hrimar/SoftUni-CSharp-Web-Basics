namespace FDMCats.App.Controllers
{
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Interfaces;
    using SimpleMvc.Framework.Controllers;

    public class HomeController : BaseController
    {
        //[HttpGet]
        //public IActionResult Index()
        //{
        //    // return View(); - old variant and not work 
        //    return RedirectToAction("/home/about");
        //}
        //[HttpGet]
        //public IActionResult About()
        //{
        //    return View(); // shows the same view like the controller's name!!!
        //    // t.e. Make the same as RedirectToAction("/home/about");
        //}

        [HttpGet]
        public IActionResult Index()
        {
            if(this.User.IsAuthenticated)
            {
                this.Model.Data["username"] = this.User.Name;
            }
           return View();  
        }
    }
}
