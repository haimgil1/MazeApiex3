using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AP2ex3.Models
{
    public interface IProductsManager
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        void AddProduct(Product p);
        void UpdateProduct(Product p);
        void DeleteProduct(int id);
    }
}