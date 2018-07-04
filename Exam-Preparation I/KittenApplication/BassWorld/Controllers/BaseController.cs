namespace FDMCats.App.Controllers
{
    using SimpleMvc.Data;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Interfaces;
    using SimpleMvc.Framework.Controllers;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Text;
    using WebServer.Http;

    public abstract class BaseController : Controller
    {
        protected override void InitializeUser()
        {
            base.InitializeUser();
            this.Model.Data["isAuthenticated"] = this.User.IsAuthenticated ? "none" : "flex";
            this.Model.Data["isLoggedIn"] = this.User.IsAuthenticated ? "flex" : "none";
            //this.Model.Data.Add("username", 
            //    string.IsNullOrWhiteSpace(this.User.Name) ? "" : this.User.Name);

            this.Model.Data["isAuthenticatedBody"] = this.User.IsAuthenticated ? "none" : "block";
            this.Model.Data["isLoggedInBody"] = this.User.IsAuthenticated ? "block" : "none";
        }

        //protected override IViewable View([CallerMemberName] string caller = "")
        //{
        //    this.Model.Data.Add("isAuthenticated", this.User.IsAuthenticated ? "none" : "flex");
        //    this.Model.Data.Add("isLoggedIn", this.User.IsAuthenticated ? "flex" : "none");
        //    //this.Model.Data.Add("username", 
        //    //    string.IsNullOrWhiteSpace(this.User.Name) ? "" : this.User.Name);

        //    return base.View(caller);
        //}

        //protected BaseController()
        //{
        //    var userIsAuthenticated = this.Request.Session.Get(SessionStore.CurrentUserKey) != null;

        //    this.Model.Data.Add("isAuthenticated", userIsAuthenticated ? "none" : "block");
        //    this.Model.Data.Add("isLoggedIn", userIsAuthenticated ? "block" : "none");

        //}


        //// instead of taking DB from the services, can from here:

        //public BaseController()
        //    : this(new BassWorldContext())
        //{
        //}
        //public BaseController(BassWorldContext dbContext)
        //{
        //    this.DbContext = dbContext;
        //}
        //public BassWorldContext DbContext { get; set; }

    }
}
