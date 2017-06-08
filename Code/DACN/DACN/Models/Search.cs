using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DACN.Models
{
    public class Search
    {
        public string Title { get; set; }

        public string Description { get; set; }
        public string Link { get; set; }

        public Search(string Title, string Description, string Link)
        {
            this.Title = Title;
            this.Description = Description;
            try
            {
                int temp = Link.LastIndexOf("/url?q=");
                Link = Link.Substring(Link.IndexOf("=") + 1);
                this.Link = Link.Substring(0,Link.IndexOf("&"));
            }
            catch {
                this.Link = Link;
            }
        }

    }
}
