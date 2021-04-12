using System;
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

            //Convert.ToInt16("");
            //or
            throw new Exception("404 not found");
            return View();
        }
    }
}