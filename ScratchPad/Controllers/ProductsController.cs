using Company.ServiceContracts;
using CompanyName.DataLayer;
using CompanyName.DomainModels;
using ScratchPad.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace ScratchPad.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Admin/Products
        private readonly EFDBFirstDatabaseEntities _dbContext;
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _dbContext = new EFDBFirstDatabaseEntities();
            _productService = productService;
        }
        // GET: 
        [MyAuthenticationFilter]
        [CustomerAuthorizationFilter]
  
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
    }
}