using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScratchPad.Models;

namespace ScratchPad.Controllers
{
    public class BrandController : Controller
    {
        public EFDBFirstDatabaseEntities DatabaseOperation()
        {
            var db = new EFDBFirstDatabaseEntities();
            return db;
        }
        // GET: Brand
        public ActionResult Index()
        {
            var db = DatabaseOperation();
            List<Brand> brands = db.Brands.ToList();
            return View(brands);
        }
    }
}