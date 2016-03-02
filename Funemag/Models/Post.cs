using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Funemag.Models
{
    public class Post
    {
        public int PostId { get; set; }

        [Display(Name = "Title")]
        [Required]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Content")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Content { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime Date { get; set; }

        [Display(Name = "View Counts")]
        public int ViewCount { get; set; }

        [Display(Name = "Hidden?")]
        public bool IsHidden { get; set; }

        public virtual ICollection<Platform> Platforms { get; set; }

        public Post()
        {
            Platforms = new List<Platform>();
        }
    }
}