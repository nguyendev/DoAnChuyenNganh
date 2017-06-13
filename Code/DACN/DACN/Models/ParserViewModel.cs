using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DACN.Models
{
    public class ParserViewModel
    {
        public static int MAX_H2_LENGTH = 30;
        public static int MAX_H3_LENGTH = 30;
        public static int MAX_H4_LENGTH = 30;
        public static int MAX_TEXT_LENGTH = 100;
        public string Link { get; set; }
        
        public string Title { get; set; }

        public string[,,,] Content { get; set; }
    
        public ParserViewModel(string Title, string Link, string[,,,] Content)
        {
            this.Title = Title;
            this.Link = Link;
            this.Content = Content;
        }
    }
}
