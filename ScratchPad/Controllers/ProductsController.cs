using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScratchPad.Models;

namespace ScratchPad.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public EFDBFirstDatabaseEntities DatabaseOperation()
        {
            var db = new EFDBFirstDatabaseEntities();
            return db;
        }
        // GET: Category
        public ActionResult Index(string searchQuery="")
        {
            ViewBag.SearchTerm = searchQuery;
            var db = DatabaseOperation();
            List<Product> products = db.Products.Where(p => p.ProductName.Contains(searchQuery)).ToList();
            return View(products);
        }

        public ActionResult Details(long id)
        {
            var db = DatabaseOperation();
            Product product = db.Products.SingleOrDefault(p => p.ProductID == id);
            return View(product);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            var db = new EFDBFirstDatabaseEntities();
            db.Products.Add(product);
            int result = db.SaveChanges();
            return RedirectToAction("Index", "Products");
        }
    }
}