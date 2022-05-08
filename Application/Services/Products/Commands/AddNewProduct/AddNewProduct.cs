using Application.Interfaces.Context;
using Common.Dto;
using Domain.Entities.Products;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;

namespace Application.Services.Products.Commands
{
    public class AddNewProduct : IAddNewProduct
    {
        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _environment;
        public AddNewProduct(IDataBaseContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public ResultDto ExecutResult(ProductDto product)
        {
            try
            {
                if (product == null)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "عملیات با خطا مواجه گردید"
                    };
                }
                var category = _context.Categories.Find(product.CategoryId);
                Product pr = new Product
                {
                    Id = product.Id,
                    Brand = product.Brand,
                    Description = product.Description,
                    Displayed = product.Displayed,
                    Inventory = product.Inventory,
                    Category = category,
                    Name = product.Name,
                    Model = product.Model,
                    Price = product.Price,
                };
                _context.Products.Add(pr);
                List<ProductFeatures> ProductFeatures = new List<ProductFeatures>();
                foreach (var item in ProductFeatures)
                {
                    ProductFeatures.Add(new ProductFeatures
                    {
                        Value = item.Value,
                        DisplayName = item.DisplayName,
                        Product = pr,
                    });
                }
                _context.ProductFeatures.AddRange(ProductFeatures);
                List<ProductImages> ProductImages = new List<ProductImages>();
                foreach (var item in product.ProductImages)
                {
                    var uploadedResult = UploadFile(item);
                    ProductImages.Add(new ProductImages
                    {
                        Product = pr,
                        Src = uploadedResult.FileNameAddress,
                    });
                }
                _context.Images.AddRange(ProductImages);
                _context.SaveChanges();

                return new ResultDto
                {
                    IsSuccess = true,
                    Message = "باموفقیت محصول ذخیره شد.",
                };
            }
            catch
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "عملیات با خطا مواجه گردید"
                };
            }


        }
        public UploadDto UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string folder = $@"images\ProductImages\";
                var uploadsRootFolder = Path.Combine(_environment.WebRootPath, folder);
                if (!Directory.Exists(uploadsRootFolder))
                {
                    Directory.CreateDirectory(uploadsRootFolder);
                }

                if (file == null || file.Length == 0)
                {
                    return new UploadDto()
                    {
                        Status = false,
                        FileNameAddress = "",
                    };
                }
                string fileName = DateTime.Now.Ticks.ToString() + file.FileName;
                var filePath = Path.Combine(uploadsRootFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                return new UploadDto()
                {
                    FileNameAddress = folder + fileName,
                    Status = true,
                };
            }
            return null;
        }
    }
}
