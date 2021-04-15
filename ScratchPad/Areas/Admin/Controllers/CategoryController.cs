using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModels;
using ScratchPad.Filters;
using ScratchPad.Models;

namespace ScratchPad.Areas.Admin.Controllers
{
    [AdminAuthorizationFilter]
    public class CategoryController : Controller
    {
        public EFDBFirstDatabaseEntities DatabaseOperation()
        {
            var db = new EFDBFirstDatabaseEntities();
            return db;
        }
        // GET: Category
        public ActionResult Index(string searchCategory = "")
        {
            ViewBag.SearchTerm = searchCategory;
            var db = DatabaseOperation();
            List<Category> categories = db.Categories.Where(cat => cat.CategoryName.Contains(searchCategory)).ToList();
            return View(categories);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category cat)
        {
            var db = new EFDBFirstDatabaseEntities();
            if (Request.Files.Count >= 1)
            {
                var file = Request.Files[0];
                string ImageName = System.IO.Path.GetFileName(file.FileName);
                string virtualPath = "~/upload-img/" + ImageName;
                string physicalPath = Server.MapPath(virtualPath);

                // save image in folder
                file.SaveAs(physicalPath);
                cat.Photo = virtualPath;
            }
            db.Categories.Add(cat);
            db.SaveChanges();
            return RedirectToAction("Index", "Category");
        }

        public ActionResult Details(long id)
        {
            Category category = DatabaseOperation().Categories.SingleOrDefault(cat => cat.CategoryID == id);
            return View(category);
        }

        public ActionResult Edit(long id)
        {
            var db = new EFDBFirstDatabaseEntities();
            Category cat = db.Categories.SingleOrDefault(c => c.CategoryID == id);
            return View(cat);
        }

        [HttpPost]
        public ActionResult Edit(Category cat)
        {
            var db = new EFDBFirstDatabaseEntities();
            Category oldCat = db.Categories.SingleOrDefault(ctr => ctr.CategoryID == cat.CategoryID);
            //Now update
            if (oldCat != null)
            {
                oldCat.CategoryName = cat.CategoryName;
            }

            db.SaveChanges();
            return RedirectToAction("Index", "Category");
        }

        public ActionResult Delete(long id)
        {
            var db = new EFDBFirstDatabaseEntities();
            Category cat = db.Categories.SingleOrDefault(ctr => ctr.CategoryID == id);
            db.Categories.Remove(cat);
            db.SaveChanges();
            return RedirectToAction("Index", "Category");
        }
    }
}