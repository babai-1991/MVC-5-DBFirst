using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScratchPad.Models;

namespace ScratchPad.Controllers
{
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
    }
}