using System;
using System.Collections.Generic;
using Company.ServiceContracts;
using CompanyName.DomainModels;
using CompanyName.RepositoryContract;

namespace Company.ServiceLayer
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<Product> GetProducts()
        {
            List<Product> products = _productRepository.GetProducts();
            return products;
        }

        public List<Product> SearchProducts(string productName)
        {
            List<Product> products = _productRepository.SearchProducts(productName);
            return products;
        }

        public Product GetProductByProductId(int productId)
        {
            Product product = _productRepository.GetProductByProductId(productId);
            return product;
        }

        public void DeleteProduct(int id)
        {
            _productRepository.DeleteProduct(id);

        }

        public void UpdateProduct(Product product)
        {
            _productRepository.UpdateProduct(product);

        }

        public void InsertProduct(Product p)
        {
            if (p.Price <= 1000000)
            {
                _productRepository.InsertProduct(p);

            }
            else
            {
                throw new Exception("Price limit exceeds");
            }
        }
    }
}
