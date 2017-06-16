using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DACN.Models.CRFsViewModels;
using Microsoft.AspNetCore.Http;

namespace DACN.Services.Session.CRFS
{
    public class SessionXacDinhDongDuLieuNoiDung
    {
        public static void SetSession(List<CXacDinhDongDLNDViewModel> list, HttpContext http)
        {
            try
            {
                int i = 0;
                foreach (var item in list)
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

        public static List<CXacDinhDongDLNDViewModel> GetSession(HttpContext http)
        {
            try
            {
                List<CXacDinhDongDLNDViewModel> list = new List<CXacDinhDongDLNDViewModel>();
                int? count = http.Session.GetInt32("number");
                if (count.HasValue)
                {
                    for (int i = 0; i < count; i++)
                    {
                        var Title = http.Session.GetString($"Title{i}");
                        var Link = http.Session.GetString($"Link{i}");
                        var Text = http.Session.GetObjectFromJson<string[]>($"Text{i}");
                        list.Add(new CXacDinhDongDLNDViewModel(Title, Link, Text));
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

