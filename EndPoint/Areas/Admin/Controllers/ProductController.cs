using Application.Interfaces.FacadPattern;
using Application.Services.Categories.FacadPattern;
using Application.Services.Products.Commands;
using Application.Services.Products.Queries.GetProductDetailAdmin;
using Common.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace testWebsit.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IFacadPattern _productFacad;
        public ProductController(IFacadPattern productFacad)
        {
            _productFacad = productFacad;
        }
        
        public IActionResult Index(int Page =1, int PageSize = 20)
        {
            return View(_productFacad.GetProductAdmin.Execute(Page,PageSize).Date);
        }
        public IActionResult Detail(long Id)
        {
            return View(_productFacad.GetProductDetailAdmin.Execut(Id).Date);
        }
        [HttpGet]
        public IActionResult AddNewProduct()
        {
            ViewBag.Category = new SelectList(_productFacad.GetAllCategory.Result().Date, "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult AddNewProduct(ProductDto request, List<ProductFeaturesDto> Features)
        {
            List<IFormFile> images = new List<IFormFile>();
            for (int i = 0; i < Request.Form.Files.Count; i++)
            {
                var file = Request.Form.Files[i];
                images.Add(file);
            }
            request.ProductImages = images;
            request.ProductFeatures = Features;
            return Json(_productFacad.AddNewProduct.ExecutResult(request));
        }
        [HttpPost]
        public IActionResult RemoveProduct(long productId)
        {
            return Json(_productFacad.RemoveProduct.ExecutResult(productId));
        }
        [HttpGet]
        public IActionResult EditProduct(long Id)
        {
            return View(_productFacad.GetProductDetailAdmin.Execut(Id).Date);
        }
        [HttpPost]
        public IActionResult EditProduct(ProductDetailDto request, List<ProductFeaturesDto> Features)
        {
            List<IFormFile> images = new List<IFormFile>();
            for (int i = 0; i < Request.Form.Files.Count; i++)
            {
                var file = Request.Form.Files[i];
                images.Add(file);
            }
            //request.Images = images;
            //request.Feachers = Features;
            return Json(_productFacad.EditProduct.resultDto(request));
        }
    }
}
