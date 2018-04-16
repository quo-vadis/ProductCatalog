using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Demo.Products.Models;

namespace Demo.Products.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        public IEnumerable<Category> GetAll()
        {
            List<Category> categories = new List<Category>();

            using (var pce = new ProductsCatalogEntities())
            {
                categories = pce.Database.SqlQuery<Category>("exec usp_CategorySelect").ToList<Category>();

            }

            return categories.Count > 0 ? categories : null;
        }
    }
}