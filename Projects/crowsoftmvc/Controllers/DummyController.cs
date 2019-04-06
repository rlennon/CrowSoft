using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crowsoftmvc.Models;
using Microsoft.AspNetCore.Mvc;
using crowsoftmvc.Data;

namespace crowsoftmvc.Controllers
{
    public class DummyController : Controller
    {
        public IActionResult Index()
        {
            DummyContext context = HttpContext.RequestServices.GetService(typeof(DummyContext)) as DummyContext;
            List<Dummy> list = context.GetAllDummys();
            return View(list);
        }
    }
}