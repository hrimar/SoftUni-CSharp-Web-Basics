using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebServer.GameStoreApplication.Data;
using WebServer.GameStoreApplication.Data.Models;
using WebServer.GameStoreApplication.Services.Contracts;
using WebServer.GameStoreApplication.ViewModels.Admin;
using WebServer.GameStoreApplication.ViewModels.Home;

namespace WebServer.GameStoreApplication.Services
{
    public class GameService : IGameService
    {

        public void Create(string title, string description,
                    string imageUrl, decimal price,
                    double size, string videoId, DateTime releaseDate)
        {
            using (var db = new GameStoreDbContext())
            {
                var game = new Game
                {
                    Title = title,
                    Description = description,
                    ImageUrl = imageUrl,
                    Price = price,
                    Size = size,
                    VideoId = videoId,
                    ReleaseDate = releaseDate
                };

                db.Add(game); // db.Games.Add(game); - NO
                db.SaveChanges();
            }
        }


        public IEnumerable<AdminListGameViewModel> All()
        {
            using (var db = new GameStoreDbContext())
            {
                // т.е. from DB take all games and return them as ViewModel:

                return db.Games.Select(g => new AdminListGameViewModel
                {
                    Id = g.Id,
                    Name = g.Title,
                    Price = g.Price,
                    Size = g.Size
                })
                .ToList();
            }
        }

        public AdminDeleteGameViewModel Find(int id)
        {
            using (var context = new GameStoreDbContext())
            {
                var game = context
                        .Games
                        .Where(g => g.Id == id)
                        .Select(g => new AdminDeleteGameViewModel
                        {
                            Id = g.Id,
                            Title = g.Title,
                            Description = g.Description,
                            ImageUrl = g.ImageUrl,
                            Price = g.Price,
                            Size = g.Size,
                            VideoId = g.VideoId,
                            ReleaseDate = g.ReleaseDate

                        })
                        .FirstOrDefault();

                return game;
            }
        }

        public bool Update(int id,
                           string title,
                           string description,
                           string image,
                           decimal price,
                           double size,
                           string videoId,
                           DateTime releaseDate)
        {
            using (var context = new GameStoreDbContext())
            {
                var game = context.Games.FirstOrDefault(g => g.Id == id);
                if (game == null)
                {
                    return false;
                }

                game.Title = title;
                game.Description = description;
                game.ImageUrl = image;
                game.Price = price;
                game.Size = size;
                game.VideoId = videoId;
                game.ReleaseDate = releaseDate;

                context.Update(game);
                context.SaveChanges();

                return true;
            }
        }


        public bool Delete(int id)
        {
            using (var context = new GameStoreDbContext())
            {
                var game = context.Games.FirstOrDefault(g => g.Id == id);

                if (game == null)
                {
                    return false;
                }

                context.Remove(game);
                context.SaveChanges();

                return true;
            }
        }



        public IList<HomeUserGameListModel> AllGames() // !!!
        {
            using (var context = new GameStoreDbContext())
            {
                return context
                       .Games
                       .Select(g => new HomeUserGameListModel
                       {
                           Id = g.Id,
                           Title = g.Title,
                           Description = g.Description.Substring(0, 300),
                           Price = g.Price,
                           Size = g.Size,
                           VideoId = g.VideoId
                       })
                       .ToList();
            }

        }

    }
}
