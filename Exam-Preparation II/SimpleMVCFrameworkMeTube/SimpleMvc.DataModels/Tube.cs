using System.ComponentModel.DataAnnotations;

namespace SimpleMvc.DataModels
{
   public class Tube
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        //[RegularExpression(@"^([A-Za-z]){7}\d{3}$")]
        //[RegularExpression(@"^(\+359|0)\d{9}$")]
        public string Title { get; set; }

        public string Author { get; set; }
              
        public string Description { get; set; }

        [Required]       
        public string YoutubeId { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Views { get; set; } //  To be 0 by default!!!

        public int UploaderId { get; set; }
        public User Uploader { get; set; }
    }
}
