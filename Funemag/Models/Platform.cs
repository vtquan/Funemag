using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Funemag.Models
{
    public class Platform
    {
        public int PlatformId { get; set; }

        public string Maker { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}