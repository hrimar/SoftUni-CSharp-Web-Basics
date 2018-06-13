using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.GameStoreApplication.ViewModels.Home
{
   public class HomeUserGameListModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public double Size { get; set; }

        public string Description { get; set; }

        public string VideoId { get; set; }
    }
}
