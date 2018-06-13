using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebServer.GameStoreApplication.Common;

namespace WebServer.GameStoreApplication.Data.Models
{
   public class Game
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ValidationConstants.Game.TitleMinLength)]
        [MaxLength(ValidationConstants.Game.TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(ValidationConstants.Game.VideoLength)]
        [MaxLength(ValidationConstants.Game.VideoLength)]
        public string VideoId { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        // in GB
        public double Size { get; set; }

        public decimal Price { get; set; }

        [Required]
        [MinLength(ValidationConstants.Game.DescriptionMinLength)]
        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }

        public ICollection<UserGame> Users { get; set; } = new List<UserGame>();


        //public int Id { get; set; }

        //[Required]
        //[MinLength(3)]
        //[MaxLength(100)]
        //public string Title { get; set; }

        //[Required]
        //[MinLength(11)]
        //[MaxLength(11)] // in order to safe space in DB!!!
        //public string VideoId { get; set; } // Trailer

        ////[Required]
        //public string Image { get; set; } // Url

        //// In GB
        //public double Size { get; set; }

        //public decimal Price { get; set; }


        //[Required]
        //[MinLength(20)]        
        //public string Description { get; set; }

        //public DateTime ReleaseDate { get; set; }

        //public ICollection<UserGame> Users { get; set; } = new List<UserGame>();
    }
}
