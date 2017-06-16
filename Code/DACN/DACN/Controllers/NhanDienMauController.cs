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
                bool daThem = false;
                try
                {
                    string source = await NhanDienMauServices.formatHtml(item.Link);
                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(source);
                    bool isMau1 = Mau1.Check(doc);
                    bool isMau2 = Mau2.Check(doc);
                    bool isMau3 = Mau3.Check(doc);
                    bool isMau4 = Mau4.Check(doc);
                    bool isMau5 = Mau5.Check(doc);
                    bool isMau6 = Mau6.Check(doc);
                    bool isMau7 = Mau7.Check(doc);
                    int result = 0;
                    if (isMau1)
                    {
                        if (isMau2)
                        {
                            if (isMau3)
                            {
                                result = Max(Mau1.GetContent(doc, item.Link).P, Mau2.GetContent(doc, item.Link).P, Mau3.GetContent(doc, item.Link).P);
                            }
                            else
                            {
                                result = Max(Mau1.GetContent(doc, item.Link).P, Mau2.GetContent(doc, item.Link).P, 1, 2);
                            }
                        }
                        else if (isMau3)
                        {
                            result = Max(Mau1.GetContent(doc, item.Link).P, Mau3.GetContent(doc, item.Link).P, 1, 3);

                        }
                        else
                            result = 1;

                    }
                    else if (isMau2)
                    {
                        if (isMau3)
                            result = Max(Mau2.GetContent(doc, item.Link).P, Mau3.GetContent(doc, item.Link).P, 2, 3);
                        else
                            result = 2;
                    }
                    else if (isMau3)
                        result = 3;
                    else if (isMau4)
                        result = 4;
                    else if(isMau5)
                        result = 5;
                    else if (isMau6)
                        result = 6;
                    else if (isMau7)
                        result = 7;
                    switch (result)
                    {
                        case 1:
                            listResult.Add(Mau1.GetContent(doc, item.Link));
                            daThem = true;
                            break;
                        case 2:
                            listResult.Add(Mau2.GetContent(doc, item.Link));
                            daThem = true;
                            break;
                        case 3:
                            listResult.Add(Mau3.GetContent(doc, item.Link));
                            daThem = true;
                            break;
                        case 4:
                            listResult.Add(Mau4.GetContent(doc, item.Link));
                            daThem = true;
                            break;
                        case 5:
                            listResult.Add(Mau5.GetContent(doc, item.Link));
                            daThem = true;
                            break;
                        case 6:
                            listResult.Add(Mau6.GetContent(doc, item.Link));
                            daThem = true;
                            break;
                        case 7:
                            listResult.Add(Mau7.GetContent(doc, item.Link));
                            daThem = true;
                            break;
                        default:
                            break;
                    }

                }
                catch { }
                if (!daThem)
                    listResult.Add(new NhanDienMauViewModel(item.Link, "Có thể nó không phải là một bài tin tức hoặc không phải là một bài tin tức chi tiết hoặc là chúng tôi chưa có mẫu để lấy dữ liệu", null, "Not Template"));
            }
            return View(listResult);
        }
        private int Max(string[] mau1, string[] mau2, int number1, int number2)
        {
            int countM1 = mau1.Count();
            int countM2 = mau2.Count();
            if (countM1 >= countM2)
                return number1;
            else
                return number2;
        }
        private int Max(string[] mau1, string[] mau2, string[] mau3)
        {
            int countM1 = mau1.Count();
            int countM2 = mau2.Count();
            int countM3 = mau3.Count();
            if (countM1 >= countM2 && countM1 >= countM3)
                return 1;
            if (countM2 >= countM1 && countM2 >= countM3)
                return 2;
            return 3;
        }
    }
}