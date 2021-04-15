using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Company.ServiceContracts;
using Company.ServiceLayer;
using CompanyName.DataLayer;
using CompanyName.DomainModels;
using ScratchPad.Filters;


namespace ScratchPad.Areas.Admin.Controllers
{
    [AdminAuthorizationFilter]
    public class ProductsController : Controller
    {
        private readonly EFDBFirstDatabaseEntities _dbContext;
        private readonly IProductService _productService;
        public ProductsController()
        {
            _dbContext = new EFDBFirstDatabaseEntities();
            _productService = new ProductService();
        }
        // GET: Category
        public ActionResult Index(string searchQuery = "", string columnName = "ProductID", string iconClass = "fa-sort-asc", int currentPageNo = 1)
        {
            ViewBag.SearchTerm = searchQuery;

            List<Product> products = _productService.SearchProducts(searchQuery);
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

        public ActionResult Details(int id)
        {

            Product product = _productService.GetProductByProductId(id);
            return View(product);
        }

        public ActionResult Create()
        {

            ViewBag.Categories = _dbContext.Categories.ToList();
            ViewBag.Brands = _dbContext.Brands.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {

                if (Request.Files.Count >= 1)
                {
                    var file = Request.Files[0];
                    var imgBytes = new Byte[file.ContentLength];
                    file.InputStream.Read(imgBytes, 0, file.ContentLength);
                    var base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length);
                    product.Photo = base64String;
                }

                _productService.InsertProduct(product);

                return RedirectToAction("Index", "Products");

            }

            return RedirectToAction("Create", "Products");
        }

        public ActionResult Edit(int id)
        {

            Product product = _productService.GetProductByProductId(id);
            ViewBag.Categories = _dbContext.Categories.ToList();
            ViewBag.Brands = _dbContext.Brands.ToList();
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(Product p)
        {
            _productService.UpdateProduct(p);
            return RedirectToAction("Index", "Products");
        }

        public ActionResult Delete(int id)
        {
            _productService.DeleteProduct(id);
            return RedirectToAction("Index", "Products");
        }
    }
}