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
    public class NewsViewModel
    {
        public News News { get; set; }

        [Display(Name = "Platforms")]
        public virtual ICollection<AssignedPlatformData> AssignedPlatforms { get; set; }

        public NewsViewModel()
        {
            this.News = new News();
            AssignedPlatforms = new Collection<AssignedPlatformData>();
        }
    }
}