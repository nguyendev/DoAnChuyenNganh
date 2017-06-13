using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DACN.Models;
using DACN.Services;

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
            return View(listParser);
        }
    }
}