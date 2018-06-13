using System;
using System.Collections.Generic;
using System.Text;
using WebServer.GameStoreApplication.ViewModels.Admin;
using WebServer.GameStoreApplication.ViewModels.Home;

namespace WebServer.GameStoreApplication.Services.Contracts
{
    public interface IGameService
    {
        void Create(string title, string description, string imageUrl, decimal price,
                            double size, string videoId, DateTime releaseDate);

        IEnumerable<AdminListGameViewModel> All(); // !!!


        AdminDeleteGameViewModel Find(int id);

        bool Update(int id,
                          string title,
                          string description,
                          string image,
                          decimal price,
                          double size,
                          string videoId,
                          DateTime releaseDate);

        bool Delete(int id);


        IList<HomeUserGameListModel> AllGames(); // !!!
    }
}
