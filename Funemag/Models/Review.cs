using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Funemag.Models
{
    public class Review : Post
    {
        [Display(Name = "Game Title")]
        public string GameTitle { get; set; }

        [Display(Name = "Info Link")]
        public string InfoLink { get; set; }

        [Display(Name = "Affiliate Link")]
        [DataType(DataType.Url)]
        public string AffiliateLink { get; set; }
    }
}