using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompanyName.DataLayer;
using CompanyName.DomainModels;
using ScratchPad.Models;

namespace ScratchPad.Controllers
{
    public class BrandController : Controller
    {
        // GET: Brand
        public EFDBFirstDatabaseEntities DatabaseOperation()
        {
            var db = new EFDBFirstDatabaseEntities();
            return db;
        }
        // GET: Brand
        public ActionResult Index(string searchBrand = "")
        {
            ViewBag.SearchTerm = searchBrand;
            var db = DatabaseOperation();
            List<Brand> brands = db.Brands.Where(b => b.BrandName.Contains(searchBrand)).ToList();
            return View(brands);
        }
    }
}