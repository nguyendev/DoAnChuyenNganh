using DACN.Models;
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
    public class CRFSServices
    {
        public static async Task<ParserViewModel> getParserAsync(SearchViewModel Search)
        {
            try
            {
                HttpClient http = new HttpClient();
                //Format UTF 8
                var link = System.Uri.UnescapeDataString(Search.Link);

                var response = await http.GetByteArrayAsync(link);
                String source = Encoding.GetEncoding("utf-8").GetString(response, 0, response.Length - 1);
                source = WebUtility.HtmlDecode(source);

                HtmlDocument document = new HtmlDocument();
                //Load trang web, nạp html vào document
                document.LoadHtml(source);


                var threadItems = document.DocumentNode.SelectNodes(".//div").ToList();
                string[,,,] Content = new string[
                    ParserViewModel.MAX_H2_LENGTH,
                    ParserViewModel.MAX_H3_LENGTH,
                    ParserViewModel.MAX_H4_LENGTH,
                    ParserViewModel.MAX_TEXT_LENGTH];
                int h2, h3, h4, text;
                h2 = h3 = h4 = text = 0;
                foreach (var item in threadItems)
                {
                    var h2Note = item.SelectNodes(".//h2");
                    if (h2Note != null)
                    {
                        foreach (var h2Item in h2Note)
                        {
                            Content[h2, 0, 0, 0] = h2Item.InnerText;
                            h2++;
                            h3 = 1;
                            var h3Note = item.SelectNodes(".//h3");
                            if (h3Note != null)
                            {
                                // Khoi tao gia tri h3 mac dinh ban dau

                                foreach (var h3Item in h3Note)
                                {
                                    Content[h2, h3, 0, 0] = h3Item.InnerText;
                                    h3++;
                                    h4 = 2;
                                    var h4Note = item.SelectNodes(".//h4");
                                    if (h4Note != null)
                                    {
                                        // Khoi tao gia tri h3 mac dinh ban dau
                                        foreach (var h4Item in h4Note)
                                        {
                                            Content[h2, h3, h4, 0] = h4Item.InnerText;
                                            h4++;
                                            var ph4Note = item.SelectNodes(".//p");
                                            text = 3;
                                            if (ph4Note != null)
                                            {

                                                foreach (var textItem in ph4Note)
                                                {
                                                    Content[h2, h3, h4, text] = h3Item.InnerText;
                                                    text++;
                                                }
                                            }
                                            var bh4Note = item.SelectNodes(".//b");
                                            if (bh4Note != null)
                                            {
                                                foreach (var textItem in bh4Note)
                                                {
                                                    Content[h2, h3, h4, text] = h3Item.InnerText;
                                                    text++;
                                                }

                                            }
                                        }

                                    }
                                    else
                                    {
                                        text = 3;
                                        var ptextNote = item.SelectNodes(".//p");
                                        if (ptextNote != null)
                                        {
                                            foreach (var pItem in ptextNote)
                                            {
                                                Content[h2, h3, h4, text] = pItem.InnerText;
                                                text++;
                                            }
                                        }
                                        var btextNote = item.SelectNodes(".//b");
                                        if (btextNote != null)
                                        {
                                            foreach (var bItem in btextNote)
                                            {
                                                Content[h2, h3, h4, text] = bItem.InnerText;
                                                text++;
                                            }
                                        }
                                    }

                                }
                            }
                            else
                            {
                                text = 3;
                                var ptextNote = item.SelectNodes(".//p");
                                if (ptextNote != null)
                                {
                                    foreach (var pItem in ptextNote)
                                    {
                                        Content[h2, h3, 0, text] = pItem.InnerText;
                                        text++;
                                    }
                                }
                                var btextNote = item.SelectNodes(".//b");
                                if (btextNote != null)
                                {
                                    foreach (var bItem in btextNote)
                                    {
                                        Content[h2, h3, 0, text] = bItem.InnerText;
                                        text++;
                                    }
                                }
                            }


                        }
                    }
                    else
                    {
                        text = 3;
                        var ptextNote = item.SelectNodes(".//p");
                        if (ptextNote != null)
                        {
                            foreach (var pItem in ptextNote)
                            {
                                Content[h2, 0, 0, text] = pItem.InnerText;
                                text++;
                            }
                        }
                        var btextNote = item.SelectNodes(".//b");
                        if (btextNote != null)
                        {
                            foreach (var bItem in btextNote)
                            {
                                Content[h2, 0, 0, text] = bItem.InnerText;
                                text++;
                            }
                        }
                    }

                }
               
                return new ParserViewModel(Search.Title, Search.Link, Content);

            }
            catch (Exception e){
                String error = e.StackTrace; 
            }
            return null;
        }


    }
}
