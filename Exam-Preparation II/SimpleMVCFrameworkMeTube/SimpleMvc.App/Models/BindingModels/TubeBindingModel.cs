
namespace SimpleMvc.App.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class TubeBindingModel
    {
        [Required]
        public string Title { get; set; }
                
        public string Author { get; set; }

        [Required]
        public string YoutubeLink { get; set; }

        public string Description { get; set; }
    }
}
