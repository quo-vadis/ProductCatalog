using Demo.Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Products.Repository
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
    }
}
