using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScratchPad.Filters;

namespace ScratchPad.Controllers
{
    public class HomeController : Controller
    {
        [MyActionFilter]
        [MyResultFilter]
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}