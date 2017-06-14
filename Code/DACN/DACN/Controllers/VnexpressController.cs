using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DACN.Models.VnexpressViewModels;
using DACN.Services;

namespace DACN.Controllers
{
    public class VnexpressController : Controller
    {
        public async Task<IActionResult> Index(string searchString)
        {
            PhanTichViewModel model = null;
            if (!String.IsNullOrEmpty(searchString))
            {
                model = await VnexpressServices.getMainContentAsync(searchString, this.HttpContext);

            }
          
            return View(model);
        }
    }
}