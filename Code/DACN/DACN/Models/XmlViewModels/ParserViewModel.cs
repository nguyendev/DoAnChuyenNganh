using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DACN.Models.XmlViewModels
{
    public class ParserViewModel
    {
        public string Link { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public ParserViewModel(string Title, string Link, string Content)
        {
            this.Title = Title;
            this.Link = Link;
            this.Content = Content;
        }
    }
}
