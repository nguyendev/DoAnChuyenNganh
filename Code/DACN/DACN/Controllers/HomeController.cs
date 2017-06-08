using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Net;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using System.Threading;
using DACN.Models;

namespace DACN.Controllers
{
    public class HomeController : Controller
    {

        public async Task<List<Search>> getContentGoogleAsync(string Search)
        {
            string url = "https://www.google.com.vn/search?num=" + 10 + "&q=" + Search +
                "&ie=utf-8&oe=utf-8";
            List<Search> listSearch= new List<Search>();
            HttpClient http = new HttpClient();
            var response = await http.GetByteArrayAsync(url);
            String source = Encoding.GetEncoding("utf-8").GetString(response, 0, response.Length - 1);
            source = WebUtility.HtmlDecode(source);
            //HtmlDocument document = new HtmlDocument();
            HtmlWeb htmlWeb = new HtmlWeb()
            {
                AutoDetectEncoding = false,
                OverrideEncoding = Encoding.UTF8  //Set UTF8 để hiển thị tiếng Việt
            };

            //Load trang web, nạp html vào document
            HtmlDocument document = htmlWeb.Load(url);
            //document.LoadHtml(source);
            ViewData["Text"] = source;
            var threadItems = document.DocumentNode.SelectNodes(".//div[@class='g']").ToList();
            foreach (var item in threadItems)
            {
                try
                {
                    var linkNode = item.SelectSingleNode(".//h3[@class='r']/a");
                    var link = linkNode.Attributes["href"].Value;
                    var text = linkNode.InnerText;
                    var Desciption = item.SelectSingleNode(".//span[@class='st']").InnerText;
                    listSearch.Add(new Search(text, Desciption, link));
                }
                catch { }
            }
            //ViewData["Text"] = items;
            ////Load các tag div trong tag div id="rso'

            //var threadItems = document.DocumentNode.QuerySelectorAll("div#rso > div").ToList();
            //if (threadItems != null)
            return listSearch;
        }
        public async Task<IActionResult> Index(string searchString)
        {

            //List<HtmlNode> toftitle = resultat.DocumentNode.Descendants().Where
            //    (x => (x.Name == "div" && x.Attributes["class"] != null &&
            //    x.Attributes["class"].Value.Contains("block_content"))).ToList();

            //var li = toftitle[6].Descendants("li").ToList();
            //foreach (var item in li)
            //{
            //    var link = item.Descendants("a").ToList()[0].GetAttributeValue("href", null);
            //    var img = item.Descendants("img").ToList()[0].GetAttributeValue("src", null);
            //    var title = item.Descendants("h5").ToList()[0].InnerText;
            //}
            var items = new List<Search>();
            if (!String.IsNullOrEmpty(searchString))
            {
                
                items =  await getContentGoogleAsync(searchString);
            }
            if(items.Count > 0)
                return View(items);
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
