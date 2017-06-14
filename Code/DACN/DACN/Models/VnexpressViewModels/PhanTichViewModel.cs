using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DACN.Models.VnexpressViewModels
{
    public class PhanTichViewModel
    {
        public string H1 { get; set; }

        public string H3 { get; set; }

        public string[] Content { get; set; }

        public string Url { get; set; }

        public PhanTichViewModel(string url, string h1, string h3, string[] content)
        {
            this.Url = url;
            this.H1 = h1;
            this.H3 = h3;
            this.Content = content;

        }
    }
}
