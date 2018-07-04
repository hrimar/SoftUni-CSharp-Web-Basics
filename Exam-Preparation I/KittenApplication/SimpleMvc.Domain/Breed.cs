using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleMvc.Domain
{   
    public class Breed
    {

        public Breed()
        {
            this.Kittens = new List<Kitten>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; } // AND ADD THE NAMES IN THE DB BY HAND !?!

        //  1. Разучи как тава в този случай!!!
        //public string StreetTranscended { get; set; }

        //public string AmericanShorthair { get; set; }

        //public string Munchkin { get; set; }

        //public string Siamese { get; set; }

        public ICollection<Kitten> Kittens { get; set; } 
    }
}
