using DACN.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DACN.Services
{
    public class SessionParser
    {
        public static void SetSession(List<ParserViewModel> listParse, HttpContext http)
        {
            try
            {
                int i = 0;
                foreach (var item in listParse)
                {
                    http.Session.SetString($"Title{i}", item.Title);
                    http.Session.SetString($"Link{i}", item.Link);
                    http.Session.SetObjectAsJson($"Text{i}", item.Text);
                    i++;
                }
                http.Session.SetInt32("number", i);
            }
            catch { }
        }

        public static List<ParserViewModel> GetSession(HttpContext http)
        {
            try
            {
                List<ParserViewModel> list = new List<ParserViewModel>();
                int? count = http.Session.GetInt32("number");
                if (count.HasValue)
                {
                    for (int i = 0; i < count; i++)
                    {
                        var Title = http.Session.GetString($"Title{i}");
                        var Link = http.Session.GetString($"Link{i}");
                        var Text = http.Session.GetObjectFromJson<string[]>($"Text{i}");
                        list.Add(new ParserViewModel(Title, Link, Text));
                    }
                }
                return list;
            }
            catch
            {
                return null;
            }
        }
    }
}
