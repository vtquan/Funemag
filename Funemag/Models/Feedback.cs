using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Funemag.Models
{
    public class Feedback
    {
        public int FeedbackId { get; set; }

        [Display(Name = "Message")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public DateTime Date { get; set; }
    }
}