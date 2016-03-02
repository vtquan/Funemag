using Funemag.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Funemag.ViewModels
{
    public class ReviewViewModel
    {
        public Review Review { get; set; }

        [Display(Name = "Platforms")]
        public virtual ICollection<AssignedPlatformData> AssignedPlatforms { get; set; }

        public ReviewViewModel()
        {
            this.Review = new Review();
            AssignedPlatforms = new Collection<AssignedPlatformData>();
        }
    }
}