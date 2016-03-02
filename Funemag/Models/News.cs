using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Funemag.Models
{
    public class News : Post
    {
        [Display(Name = "Source")]
        public string SourceLink { get; set; }
    }
}