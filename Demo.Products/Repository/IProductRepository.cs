using Demo.Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Products.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        bool Update(Product item);
        void Add(Product item);
    }
}
