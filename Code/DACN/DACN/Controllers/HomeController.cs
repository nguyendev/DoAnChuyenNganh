using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Net;
using HtmlAgilityPack;
using System.Xml;
using DACN.Models;
using DACN.Services;
using System.Xml;
using Microsoft.CodeAnalysis;
using System.IO;

namespace DACN.Controllers
{
    public class HomeController : Controller
    {
      
        public async Task<List<SearchViewModel>> getContentGoogleAsync(string Search, int number)
        {
            string url = "https://www.google.com.vn/search?num=" + number + "&q=" + Search +
                "&ie=utf-8&oe=utf-8";
            List<SearchViewModel> listSearch= new List<SearchViewModel>();
            HttpClient http = new HttpClient();
            var response = await http.GetByteArrayAsync(url);
            String source = Encoding.GetEncoding("utf-8").GetString(response, 0, response.Length - 1);
            source = WebUtility.HtmlDecode(source);
            HtmlDocument document = new HtmlDocument();
          

            // Load trang web, nạp html vào document
            // HtmlDocument document = htmlWeb.Load(url);
            document.LoadHtml(source);
            ViewData["Text"] = source;
            var threadItems = document.DocumentNode.SelectNodes(".//div[@class='g']").ToList();
            foreach (var item in threadItems)
            {
                try
                {
                    var linkNode = item.SelectSingleNode(".//h3[@class='r']/a");
                    var link = linkNode.Attributes["href"].Value;
                    //Format UTF 8
                    link = System.Uri.UnescapeDataString(link);
                    var title = linkNode.InnerText;
                    var Desciption = item.SelectSingleNode(".//span[@class='st']").InnerText;
                    if(link.Contains("http"))
                        listSearch.Add(new SearchViewModel(title, Desciption, link));
                }
                catch { }
            }
            //ViewData["Text"] = items;
            ////Load các tag div trong tag div id="rso'

            //var threadItems = document.DocumentNode.QuerySelectorAll("div#rso > div").ToList();
            //if (threadItems != null)
            SessionGoogle.SetSessionGoogleSearch(listSearch, this.HttpContext);
            return listSearch;
        }
      
        

        public async Task<IActionResult> Index(string searchString, string number)
        {
           

            
            var items = new List<SearchViewModel>();
            if (!String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(number))
            {
                ViewData["currentNumber"] = Int32.Parse(number);
                items =  await getContentGoogleAsync(searchString, Int32.Parse(number));

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
