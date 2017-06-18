using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AP2ex3.Models;

namespace AP2ex3.Controllers
{
    public class ProductsController : ApiController
    {
        private IProductsManager productsManager = new ProductsManager();

        // GET: api/Products
        public IEnumerable<Product> GetAllProducts()
        {
            return productsManager.GetAllProducts();
        }

        // GET: api/Products/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Products
        [HttpPost]
        public Product AddProduct(Product p)
        {
            productsManager.AddProduct(p);
            return p;
        }

        // PUT: api/Products/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Products/5
        public void Delete(int id)
        {
        }
    }
}









