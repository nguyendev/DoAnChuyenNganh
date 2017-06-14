using DACN.Models.VnexpressViewModels;
using HtmlAgilityPack;
using DACN.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace DACN.Services
{
    public class VnexpressServices
    {
        public static async Task<PhanTichViewModel> getMainContentAsync(string url, HttpContext httpContext)
        {
            try
            {
                HttpClient http = new HttpClient();
                var response = await http.GetByteArrayAsync(url);
                String source = Encoding.GetEncoding("utf-8").GetString(response, 0, response.Length - 1);
                source = WebUtility.HtmlDecode(source);
                HtmlDocument document = new HtmlDocument();


                // Load trang web, nạp html vào document
                // HtmlDocument document = htmlWeb.Load(url);
                string h1 = "";
                string h3 = "";
                string[] content = new string[30];
                int number = 0;
                document.LoadHtml(source);
                var h1Note = document.DocumentNode.SelectSingleNode(".//div[@class='title_news']/h1");
                if (h1Note != null)
                {
                    h1 = h1Note.InnerText;
                }

                var h3Note = document.DocumentNode.SelectSingleNode(".//h3[@class='short_intro txt_666']");
                if (h3Note != null)
                {
                    h3 = h3Note.InnerText;
                }
                var listP = document.DocumentNode.SelectNodes(".//div[@class='fck_detail width_common block_ads_connect']/p").ToList();
                if (listP != null)
                {
                    foreach (var p in listP)
                    {
               
                        content[number] = p.InnerText;
                        number++;
                    }
                }
                PhanTichViewModel model = new PhanTichViewModel(url, h1, h3, content);
                httpContext.Session.SetObjectAsJson("PhanTich", model);

                return model;
            }
            catch
            {
                return null;
            }
        }
    }
}
    
