
using System.ComponentModel.DataAnnotations;

namespace FDMCats.App.Models.ViewModels
{
   public class AllKittensViewModel
    {
        public string Image { get; set; } = "https://www.google.bg/imgres?imgurl=https%3A%2F%2F4fi8v2446i0sw2rpq2a3fg51-wpengine.netdna-ssl.com%2Fwp-content%2Fuploads%2F2016%2F06%2FKittenProgression-Darling-week3.jpg&imgrefurl=https%3A%2F%2Fwww.alleycat.org%2Fresources%2Fhow-old-is-that-kitten-guide-three-weeks%2F&docid=aPA29PC7djHZjM&tbnid=ipzf6g_FZYI-MM%3A&vet=10ahUKEwiqt5LtpOnbAhXKDuwKHfXHCDsQMwg5KAgwCA..i&w=300&h=201&bih=907&biw=1341&q=kittens&ved=0ahUKEwiqt5LtpOnbAhXKDuwKHfXHCDsQMwg5KAgwCA&iact=mrc&uact=8";

        [Required]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string Breed { get; set; }
    }
}
