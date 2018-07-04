using FDMCats.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using SimpleMvc.Data;
using SimpleMvc.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
//using FDMCats.App.Models.ViewModels;

namespace FDMCats.Services
{
    public class KittenBusinessService : IKittenBusinessService
    {
        private readonly KittenDbContext dbContex;

        public KittenBusinessService()
        {
            this.dbContex = new KittenDbContext();
        }

        public bool Add(string name, int age, string breed)
        {
            Breed katsBreed = null;

            using (this.dbContex)
            {
                // First we Inserted the breeds by hand
                katsBreed = dbContex.Breeds.FirstOrDefault(b => b.Name == breed);

                if (katsBreed != null)
                {
                    var newKitten = new Kitten
                    {
                        Name = name,
                        Age = age,
                        Breed = katsBreed
                    };

                    this.dbContex.Kittens.Add(newKitten);
                    this.dbContex.SaveChanges();

                    return true;
                }
            }

            return false;
        }


        public List<string> All()
        {
            List<string> kittens = null; // CAN NOT ACCES VIEWMODELS IN THE APP !?!
            //using (var dbContext = new KittenDbContext())
            //{
            //    kittens = dbContext.Kittens
            //    .Include(k => k.Breed)
            //  .Select(k => new AllKittensViewModel()
            //  {
            //      Name = k.Name,
            //      Age = k.Age,
            //      Breed = k.Breed.Name
            //  })
            //   .Select(vm => $"<div>Name: {vm.Name}; Age: {vm.Age}; Breed: {vm.Breed}</div>")
            //  .ToList();
            //}

            return kittens;
        }
    }
}
