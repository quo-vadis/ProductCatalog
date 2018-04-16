using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Web;
using Demo.Products.Models;

namespace Demo.Products.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICategoryRepository _categoryrepository;

        public ProductRepository(ICategoryRepository categoryRepository)
        {
            _categoryrepository = categoryRepository;
        }

        public void Add(Product item)
        {
            using (var pce = new ProductsCatalogEntities())
            {
                var t = pce.usp_ProductInsert(item.Name, item.Code, item.Description, item.Price, item.Amount, item.CategoryId.ToString());
            }
        }

        public IEnumerable<Product> GetAll()
        {
            List<Product> products = new List<Product>();

            using (var pce = new ProductsCatalogEntities())
            {
                var categories = pce.Database.SqlQuery<Category>("exec usp_CategorySelect").ToList<Category>();
                products = pce.Database.SqlQuery<Product>("exec usp_ProductSelect").ToList<Product>();

                foreach (var product in products)
                {
                    var category = categories.FirstOrDefault(x => x.Id == product.CategoryId);
                    product.Category = new Category
                    {
                        Id = (int)product.CategoryId,
                        Name = category != null ? category.Name : "no category"
                    };
                }
            }

            return products.Count > 0 ? products : null;
        }

        public bool Update(Product item)
        {
            using (var pce = new ProductsCatalogEntities())
            {
                var category = pce.Products.FirstOrDefault(x => x.Code == item.Code);
                if (category != null)
                {
                    var t = pce.usp_ProductUpdate(item.Name, item.Code, item.Description, item.Price, item.Amount, category.CategoryId);
                }
            }
            return true;
        }
    }
}