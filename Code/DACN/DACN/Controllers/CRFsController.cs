using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DACN.Models;
using DACN.Services;
using DACN.Services.Session.CRFS;
using DACN.Models.CRFsViewModels;

namespace DACN.Controllers
{
    public class CRFsController : Controller
    {
        [Route("CRFs/buoc-1")]
        public async Task<IActionResult> Parser()
        {
            List<SearchViewModel> listSearch = SessionGoogle.GetSessionGoogleSearch(this.HttpContext);
            List<ParserViewModel> listParser = new List<ParserViewModel>();
            if (ModelState.IsValid)
            {
                foreach (var item in listSearch)
                {

                    listParser.Add(await CRFSServices.getParserAsync(item));
                }
            }
            SessionParser.SetSession(listParser, this.HttpContext);
            return View(listParser);
        }
        [Route("CRFs/Buoc-2")]
        //Xac dinh dong du lieu noi dung
        public IActionResult XDDDLNDA()
        {
            List<ParserViewModel> oldList = SessionParser.GetSession(this.HttpContext);
            List<CXacDinhDongDLNDViewModel> newList = new List<CXacDinhDongDLNDViewModel>();
            foreach (var item in oldList)
            {
                newList.Add(CRFSServices.KetNoiThanhCumDuLieuAsync(item));
            }
            SessionXacDinhDongDuLieuNoiDung.SetSession(newList, this.HttpContext);
            return View(newList);
        }
        [Route("CRFs/Buoc-3")]
        public IActionResult KetNoiThanhCumDuLieu()
        {
            List<CXacDinhDongDLNDViewModel> oldList = SessionXacDinhDongDuLieuNoiDung.GetSession(this.HttpContext);
            List<CXacDinhDongDLNDViewModel> newList = new List<CXacDinhDongDLNDViewModel>();
            foreach (var item in oldList)
            {
                newList.Add(CRFSServices.KetNoiThanhCumDuLieuAsync((ParserViewModel)item));
            }
            return View();
        }
    }
}