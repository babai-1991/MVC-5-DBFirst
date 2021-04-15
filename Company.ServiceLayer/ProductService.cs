using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.ServiceContracts;
using CompanyName.DataLayer;
using CompanyName.DomainModels;

namespace Company.ServiceLayer
{
    public class ProductService : IProductService
    {
        private EFDBFirstDatabaseEntities _dbContext;

        public ProductService()
        {
            _dbContext = new EFDBFirstDatabaseEntities();
        }

        public List<Product> GetProducts()
        {
            List<Product> products = _dbContext.Products.ToList();
            return products;
        }

        public List<Product> SearchProducts(string productName)
        {
            List<Product> products = _dbContext.Products.Where(p => p.ProductName.Contains(productName)).ToList();
            return products;
        }

        public Product GetProductByProductId(int productId)
        {
            Product product = _dbContext.Products.FirstOrDefault(p => p.ProductID == productId);
            return product;
        }

        public void DeleteProduct(int id)
        {
            Product product = _dbContext.Products.FirstOrDefault(p => p.ProductID == id);
            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            Product existingProduct = _dbContext.Products.SingleOrDefault(p => p.ProductID == product.ProductID);
            //Now update
            if (existingProduct != null)
            {
                existingProduct.ProductName = product.ProductName;
                existingProduct.Price = product.Price;
                existingProduct.DateOfPurchase = product.DateOfPurchase;
                existingProduct.AvailabilityStatus = product.AvailabilityStatus;
                existingProduct.CategoryID = product.CategoryID;
                existingProduct.BrandID = product.BrandID;
                existingProduct.Active = product.Active ?? false;
            }
            //image upload

            existingProduct.Photo = product.Photo;


            _dbContext.SaveChanges();

        }

        public void InsertProduct(Product p)
        {
            _dbContext.Products.Add(p);
            _dbContext.SaveChanges();
        }
    }
}
