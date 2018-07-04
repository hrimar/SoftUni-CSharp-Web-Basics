namespace SimpleMvc.App.Services
{
    using Microsoft.EntityFrameworkCore;
    using SimpleMvc.App.Models.ViewModels;
    using SimpleMvc.Data;
    using SimpleMvc.DataModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;


    public class TubeBusinessService
    {
        private readonly ExamDbContext dbContex;

        public TubeBusinessService()
        {
            this.dbContex = new ExamDbContext();
        }

        public bool Register(string title, string author, string youtubeLink, string description, int usersId)
        {
            var newTube = new Tube
            {
                Title = title,
                Author = author,
                YoutubeId = youtubeLink,
                Description = description,
                UploaderId = usersId
            };


            using (this.dbContex)
            {
                this.dbContex.Tubes.Add(newTube);
                this.dbContex.SaveChanges();
            }

            return true;
        }

        public Tube GetTubeById(int tubeId)
        {
            var tube = this.dbContex.Tubes.FirstOrDefault(u => u.Id == tubeId);

            return tube;
        }

        public List<Tube> GetAllTubes()
        {
            var allTubes = this.dbContex.Tubes.ToList();

            return allTubes;
        }

        public List<TubeProfileViewModel> GetTubesOfUser(int userId)
        {
            //var tubesOfUser = this.dbContex.Users
            //    .Where(u => u.Id == userId)
            //    .Include(u => u.Tubes)
            //    .Select(u => new TubesOfUserViewModel
            //    {
            //        UserId = u.Id,
            //        TubesProfiles = u.Tubes.Select(t => new TubeProfileViewModel
            //        {
            //            Title = t.Title,
            //            Author = t.Author,
            //            TubeId = t.Id
            //        }).ToList()
            //    }).First();

            var tubesOfUser = this.dbContex.Tubes
               .Where(t => t.UploaderId == userId)
               .Select(t => new TubeProfileViewModel
               {
                   Title = t.Title,
                   Author = t.Author,
                   TubeId = t.Id
               }).ToList();


            return tubesOfUser;
        }


    }
}
