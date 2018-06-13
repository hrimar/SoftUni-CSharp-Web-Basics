using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.GameStoreApplication.ViewModels.Admin
{
    public class AdminListGameViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public double Size { get; set; }
    }
}
