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
            var db = new EFDBFirstDatabaseEntities();
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Brands = db.Brands.ToList();
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

        public ActionResult Edit(long id)
        {
            var db = new EFDBFirstDatabaseEntities();
            Product product = db.Products.SingleOrDefault(p => p.ProductID == id);
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Brands = db.Brands.ToList();
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(Product p)
        {
            var db = new EFDBFirstDatabaseEntities();
            Product oldProduct = db.Products.SingleOrDefault(pr => pr.ProductID == p.ProductID);
            //Now update
            if (oldProduct != null)
            {
                oldProduct.ProductName = p.ProductName;
                oldProduct.Price = p.Price;
                oldProduct.DateOfPurchase = p.DateOfPurchase;
                oldProduct.AvailabilityStatus = p.AvailabilityStatus;
                oldProduct.CategoryID = p.CategoryID;
                oldProduct.BrandID = p.BrandID;
                oldProduct.Active = p.Active??false;
            }

            db.SaveChanges();
            return RedirectToAction("Index","Products");
        }

        public ActionResult Delete(long id)
        {
            var db = new EFDBFirstDatabaseEntities();
            Product product = db.Products.SingleOrDefault(pr => pr.ProductID == id);
            //deletion
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index", "Products");
        }
    }
}