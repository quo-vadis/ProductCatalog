using Demo.Products.CustomFilters;
using Demo.Products.Models;
using Demo.Products.Repository;
using FileHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo.Products.Controllers
{
    public class AdministratorController : Controller
    {
        private readonly IProductRepository _productRepository;

        public AdministratorController(IProductRepository repository)
        {
            _productRepository = repository;
        }

        [AuthLog(Roles = "Administrator")]
        public ActionResult Index()
        {
            ViewData["Message"] = "This action can perform only administrator";
            return View();
        }

        public ActionResult UploadFile()
        {
            ViewData["Message"] = "File upload failed!!";
            return View();
        }

        [AuthLog(Roles = "Administrator")]
        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            ViewData["Message"] = "File upload failed!!";
            try
            {
                if (file.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string path = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName);
                    file.SaveAs(path);

                    var engine = new FileHelperEngine<Product>();
                    var records = engine.ReadFile(path);

                    var products = _productRepository.GetAll();

                    foreach (var record in records)
                    {
                        if (products != null && products.Where(x => x.Code == record.Code).ToList().Count > 0)
                        {
                            UpdateProduct(record);
                        }
                        else
                        {
                            InsertRecord(record);
                        }
                    }
                }
                ViewData["Message"] = "File Uploaded Successfully!!";
                return RedirectToAction("Index", "Product");
            }
            catch(Exception ex)
            {
                ViewData["Message"] = "File upload failed during parsing";
                return View("Error");
            }
        }

        private void InsertRecord(Product product)
        {
            _productRepository.Add(product);
        }

        private void UpdateProduct(Product product)
        {
            _productRepository.Update(product);
        }

    }
}