using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DACN.Services
{
    public class ParserExtension
    {
        public static int Count;
        public static string[] Text;


        public static async Task GetText(string url)
        {
            HttpClient http = new HttpClient();
            var link = System.Uri.UnescapeDataString(url);
            var response = await http.GetByteArrayAsync(link);
            String source = Encoding.GetEncoding("utf-8").GetString(response, 0, response.Length - 1);
            source = WebUtility.HtmlDecode(source);
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(source);
            document = HtmlAgilityPackServices.RemoveScripts(document);
            var text = document.DocumentNode.SelectNodes("//body//text()").Select(node => node.InnerText);
            StringBuilder output = new StringBuilder();
            string[] temp = new string[2000];
            Count = 0;
            foreach (string line in text)
            {
                if (!line.Contains("\n"))
                { 
                    temp[Count] = line;
                    Count++;
                }
            }
            Text = new string[Count];
            for (int i = 0; i < Count; i++)
            {
                Text[i] = temp[i];
            }
           // string textOnly = WebUtility.HtmlDecode(output.ToString());

        }
        
    }
}
