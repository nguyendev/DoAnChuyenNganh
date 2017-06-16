using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DACN.Models.NhanDienMauViewModels
{
    public class NhanDienMauViewModel
    {
        public string Url { get; set; }

        public string H1 { get; set; }

        public string[] P {get;set;}

        public string Template { get; set; }

        public NhanDienMauViewModel(string Url, string H1, string[] P, string Template)
        {
            this.Url = Url;
            this.H1 = H1;
            this.P = P;
            this.Template = Template;
        }
    }
}
