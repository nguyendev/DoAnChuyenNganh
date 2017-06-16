using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DACN.Models;
using DACN.Services;
using HtmlAgilityPack;
using DACN.Services.NhanDienMau;
using DACN.Models.NhanDienMauViewModels;

namespace DACN.Controllers
{
    public class NhanDienMauController : Controller
    {
       [Route("nhan-dien-mau/ket-qua")]
        public async Task<IActionResult> Index()
        {
            List<SearchViewModel> listSearch = SessionGoogle.GetSessionGoogleSearch(this.HttpContext);
            List<NhanDienMauViewModel> listResult = new List<NhanDienMauViewModel>();
            foreach(var item in listSearch)
            {
                try
                {
                    string source = await NhanDienMauServices.formatHtml(item.Link);
                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(source);
                    bool isMau1 = Mau1.Check(doc);
                    if (isMau1)
                        listResult.Add(Mau1.GetContain(doc, item.Link));
                }
                catch { }
            }
            return View(listResult);
        }
    }
}