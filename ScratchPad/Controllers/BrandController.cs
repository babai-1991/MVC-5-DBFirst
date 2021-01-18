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
        public ActionResult Index(string searchBrand = "")
        {
            ViewBag.SearchTerm = searchBrand;
            var db = DatabaseOperation();
            List<Brand> brands = db.Brands.Where(b => b.BrandName.Contains(searchBrand)).ToList();
            return View(brands);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Brand brand)
        {
            var db = new EFDBFirstDatabaseEntities();
            db.Brands.Add(brand);
            db.SaveChanges();
            return RedirectToAction("Index", "Brand");
        }

        public ActionResult Details(long id)
        {
            Brand brand = DatabaseOperation().Brands.SingleOrDefault(b => b.BrandID == id);
            return View(brand);
        }

        public ActionResult Delete(long id)
        {
            var db = new EFDBFirstDatabaseEntities();
            Brand brand = db.Brands.SingleOrDefault(br => br.BrandID == id);
            db.Brands.Remove(brand);
            db.SaveChanges();
            return RedirectToAction("Index", "Brand");
        }

        public ActionResult Edit(long id)
        {
            var db = new EFDBFirstDatabaseEntities();
            Brand brand = db.Brands.SingleOrDefault(b => b.BrandID == id);
            return View(brand);
        }

        [HttpPost]
        public ActionResult Edit(Brand brand)
        {
            var db = new EFDBFirstDatabaseEntities();
            Brand oldBrand = db.Brands.SingleOrDefault(br => br.BrandID == brand.BrandID);
            //Now update
            if (oldBrand != null)
            {
                oldBrand.BrandName = brand.BrandName;
            }

            db.SaveChanges();
            return RedirectToAction("Index", "Brand");
        }
    }
}