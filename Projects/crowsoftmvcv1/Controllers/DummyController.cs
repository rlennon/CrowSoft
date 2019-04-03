using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crowsoftmvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace crowsoftmvc.Controllers
{
    public class DummyController : Controller
    {
        public IActionResult Index()
        {
            CrowsoftContext context = HttpContext.RequestServices.GetService(typeof(CrowsoftContext)) as CrowsoftContext;
            List<Dummy> list = context.GetAllDummys();
            return View(list);
        }
    }
}