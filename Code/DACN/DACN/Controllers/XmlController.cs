using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DACN.Models;
using DACN.Services;

namespace DACN.Controllers
{
    public class XmlController : Controller
    {
        [Route("Xml/buoc-1")]
        public async Task<IActionResult> convertXML()
        {
            List<SearchViewModel> listSearch = SessionGoogle.GetSessionGoogleSearch(this.HttpContext);
            List<DACN.Models.XmlViewModels.ParserViewModel> listParser = new List<DACN.Models.XmlViewModels.ParserViewModel>();
            
                foreach (var item in listSearch)
                {
                    //listParser.Add(await getParserAsync(item));
                    XML xml = new XML();
                    listParser.Add(await xml.convertXML(item));
                }
            return View(listParser);
        }
    }
}