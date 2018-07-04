using FDMCats.App.Models.BindingModels;
using FDMCats.App.Models.ViewModels;
using FDMCats.Services;
using FDMCats.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using SimpleMvc.Data;
using SimpleMvc.Domain;
using SimpleMvc.Framework.Attributes.Methods;
using SimpleMvc.Framework.Attributes.Security;
using SimpleMvc.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FDMCats.App.Controllers
{
    public class KittensController : BaseController
    {
        private readonly IKittenBusinessService kittenBusinessService;

        public KittensController()
        {
            this.kittenBusinessService = new KittenBusinessService();
        }


        [HttpGet]
        [PreAuthorize] // IN ORDER NOT TO GO GESTS TO THIS PAGE!
        public IActionResult Add()
        {
            this.Model.Data["error"] = string.Empty; // TO HIDE THE ERROR MESSAGE!!!
            return View();
        }

        [HttpPost]
        public IActionResult Add(KittenViewModel viewModel)
        {
            // 1. Validate attributes:
            bool modelIsValid = this.IsValidModel(viewModel);

            // 2. Create new user with Hash password and Add user to DB:
            bool isKitten = this.kittenBusinessService.Add(viewModel.Name, viewModel.Age, viewModel.Breed);

            // 3. Login the user so session:
            if (modelIsValid && isKitten)
            {
                return this.RedirectToAction("//kittens/all");
            }

            this.Model.Data["error"] = "Invalid nam, age or breed";
            return View();

        }


        // MAKE HTML STRING HERE AS CAN NOT USE SERVICES FOR THE VIEWMODELS:!!!!
        [HttpGet]
        [PreAuthorize] // IN ORDER NOT TO GO GESTS TO THIS PAGE!
        public IActionResult All() 
        {
             var kitten = this.kittenBusinessService.All();

            using (var dbContext = new KittenDbContext())
            {
              var  kittens = dbContext.Kittens
               .Include(k => k.Breed)
             .Select(k => new AllKittensViewModel()
             {
                 Name = k.Name,
                 Age = k.Age,
                 Breed = k.Breed.Name
                
                 })
              .Select(vm =>
              // $@"<div>  <img class=""img-thumbnail"" src=""https://images.pexels.com/photos/20787/pexels-photo.jpg?auto=compress&cs=tinysrgb&h=350"" alt=""{vm.Name}'s photo"" /><br/> Name: {vm.Name};  <br/>Age: {vm.Age};  <br/>Breed: {vm.Breed}</div>")
               $@"<div class=""col-4"">
                        <img class=""img-thumbnail"" src=""https://images.pexels.com/photos/20787/pexels-photo.jpg?auto=compress&cs=tinysrgb&h=350"" alt=""{vm.Name}'s photo"" />
                        <div>
                            <h5>Name: {vm.Name}</h5>
                            <h5>Age: {vm.Age}</h5>
                            <h5>Breed: {vm.Breed}</h5>
                        </div>
                    </div>")
             .ToList();

                var kittensResult = new StringBuilder();
                kittensResult.Append(@"<div class=""row text-center"">");
                for (int i = 0; i < kittens.Count; i++)
                {
                    kittensResult.Append(kittens[i]);

                    if (i % 3 == 3 - 1)
                    {
                        kittensResult.Append(@"</div><div class=""row text-center"">");
                    }
                }

                kittensResult.Append("</div>");

                this.Model.Data["kittens"] = kittensResult.ToString();
           
            }
                       
            return this.View();
        }
    }
}
