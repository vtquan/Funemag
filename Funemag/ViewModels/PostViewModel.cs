using Funemag.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Funemag.ViewModels
{
    public class PostViewModel<T> where T : class
    {
        public T Post { get; set; }

        [Display(Name = "Platforms")]
        public virtual ICollection<AssignedPlatformData> AssignedPlatforms { get; set; }
    }
}