using System.Collections.Generic;

namespace SimpleMvc.App.Models.ViewModels
{
  public  class TubesOfUserViewModel
    {
        public int UserId { get; set; }

      public List<TubeProfileViewModel> TubesProfiles { get; set; }

    }
}
