using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScratchPad.Models;

namespace ScratchPad.Areas.Manager.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Manager/Products
        public EFDBFirstDatabaseEntities DatabaseOperation()
        {
            var db = new EFDBFirstDatabaseEntities();
            return db;
        }
        // GET: Category
        public ActionResult Index(string searchQuery = "", string columnName = "ProductID", string iconClass = "fa-sort-asc", int currentPageNo = 1)
        {
            ViewBag.SearchTerm = searchQuery;
            var db = DatabaseOperation();
            List<Product> products = db.Products.Where(p => p.ProductName.Contains(searchQuery)).ToList();

            /*****************
             * Sorting
             *****************
             */
            ViewBag.IconClass = iconClass;
            ViewBag.SortColumn = columnName;

            if (ViewBag.SortColumn == "ProductID")
            {
                products = ViewBag.IconClass == "fa-sort-asc" ? products.OrderBy(p => p.ProductID).ToList() : products.OrderByDescending(p => p.ProductID).ToList();
            }
            else if (ViewBag.SortColumn == "ProductName")
            {
                products = ViewBag.IconClass == "fa-sort-asc" ? products.OrderBy(p => p.ProductName).ToList() : products.OrderByDescending(p => p.ProductName).ToList();
            }
            else if (ViewBag.SortColumn == "Price")
            {
                products = ViewBag.IconClass == "fa-sort-asc" ? products.OrderBy(p => p.Price).ToList() : products.OrderByDescending(p => p.Price).ToList();
            }
            else if (ViewBag.SortColumn == "AvailabilityStatus")
            {
                products = ViewBag.IconClass == "fa-sort-asc" ? products.OrderBy(p => p.AvailabilityStatus).ToList() : products.OrderByDescending(p => p.AvailabilityStatus).ToList();
            }
            else if (ViewBag.SortColumn == "DateOfPurchase")
            {
                products = ViewBag.IconClass == "fa-sort-asc" ? products.OrderBy(p => p.DateOfPurchase).ToList() : products.OrderByDescending(p => p.DateOfPurchase).ToList();
            }
            else if (ViewBag.SortColumn == "Brand")
            {
                products = ViewBag.IconClass == "fa-sort-asc" ? products.OrderBy(p => p.Brand.BrandName).ToList() : products.OrderByDescending(p => p.Brand.BrandName).ToList();
            }
            else if (ViewBag.SortColumn == "Category")
            {
                products = ViewBag.IconClass == "fa-sort-asc" ? products.OrderBy(p => p.Category.CategoryName).ToList() : products.OrderByDescending(p => p.Category.CategoryName).ToList();
            }

            /**************
             * Paging
             **************
             */
            int noOfRecordsPerPage = 5;
            int totalNoOfPages = Convert.ToInt16(Math.Ceiling(Convert.ToDouble(products.Count) / noOfRecordsPerPage));
            int noOfRecordsToSkip = (currentPageNo - 1) * noOfRecordsPerPage;
            ViewBag.CurrentPageNo = currentPageNo;
            ViewBag.TotalNoOfPages = totalNoOfPages;
            products = products.Skip(noOfRecordsToSkip).Take(noOfRecordsPerPage).ToList();
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
            if (ModelState.IsValid)
            {
                var db = new EFDBFirstDatabaseEntities();
                if (Request.Files.Count >= 1)
                {
                    var file = Request.Files[0];
                    var imgBytes = new Byte[file.ContentLength];
                    file.InputStream.Read(imgBytes, 0, file.ContentLength);
                    var base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length);
                    product.Photo = base64String;
                }

                db.Products.Add(product);
                int result = db.SaveChanges();
                return RedirectToAction("Index", "Products");

            }

            return RedirectToAction("Create", "Products");
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
                oldProduct.Active = p.Active ?? false;
            }
            //image upload
            if (Request.Files.Count >= 1)
            {
                var file = Request.Files[0];
                var imgBytes = new Byte[file.ContentLength];
                file.InputStream.Read(imgBytes, 0, file.ContentLength);
                var base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length);
                oldProduct.Photo = base64String;
            }

            db.SaveChanges();
            return RedirectToAction("Index", "Products");
        }
    }
}