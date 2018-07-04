using System.ComponentModel.DataAnnotations;

namespace FDMCats.App.Models.BindingModels
{
    public class KittenViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string Breed { get; set; }
    }
}
