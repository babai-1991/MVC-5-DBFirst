using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.DomainModels;

namespace Company.ServiceContracts
{
    public interface IProductService
    {
        List<Product> GetProducts();
        List<Product> SearchProducts(string productName);
        Product GetProductByProductId(int productId);
        void InsertProduct(Product p);
        void UpdateProduct(Product p);
        void DeleteProduct(int productId);
    }
}
