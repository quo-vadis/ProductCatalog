using Demo.Products.Models;
using Demo.Products.Repository;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo.Products.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductController(IProductRepository productRepo, ICategoryRepository categoryRepo)
        {
            _productRepository = productRepo;
            _categoryRepository = categoryRepo;
        }

        // GET: Product
        public ActionResult Index(string category, string searchQuery, int? page)
        {
            if (searchQuery != null)
            {
                page = 1;
            }
            else
            {
                searchQuery = "";
            }

            GetCategories();
            var products = _productRepository.GetAll();

            if (!String.IsNullOrEmpty(category))
            {
                products = products.Where(x => x.Category.Name == category).ToList();
            }

            if (!String.IsNullOrEmpty(searchQuery))
            {
                products = products.Where(x => x.Name.Contains(searchQuery)).ToList();
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            if (products != null)
                return View(products.ToPagedList(pageNumber, pageSize));
            else
                return View();
        }

        //[AuthLog(Roles = "Guest")]
        public ActionResult Create()
        {
            ViewBag.Message = "This View is designed for the Sales Executive to Save Products";
            GetCategories();
            var product = new Product();
            return View(product);
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            _productRepository.Add(product);

            return RedirectToAction("Index");
        }

        //[AuthLog(Roles = "Administrator")]
        public ActionResult SaleProduct()
        {
            ViewBag.Message = "This View is designed for the Sales Executive to Save Products";
            return View();
        }

        private void GetCategories()
        {
            var list = new List<SelectListItem>();
            var categories = _categoryRepository.GetAll().ToList();
            if (categories.Count > 0)
            {
                foreach (var item in categories)
                {
                    list.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                }
                ViewData["Category"] = list;
            }
            else
            {
                ViewData["Category"] = null;
            }
        }
    }
}