using Application.Interfaces.Context;
using Application.Services.Products.Queries.GetProductDetailAdmin;
using Common.Dto;
using Domain.Entities.Products;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Products.Commands.EditProduct
{
    public interface IEditProduct
    {
        ResultDto resultDto(ProductDetailDto model);
    }
    public class EditProduct : IEditProduct
    {
        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _environment;
        public EditProduct(IDataBaseContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
            
        }
        public ResultDto resultDto(ProductDetailDto model)
        {
            var res = _context.Products.FirstOrDefault(p=>p.Id == model.Id);
            //var resCat = _context.Categories.FirstOrDefault(c => c.Id == model.CategoryId);
            res.Name = model.Name;  
            res.Brand = model.Brand;
            res.UpdateDateTime = DateTime.Now;
            res.Inventory = model.Inventory;
            //res.Price = model.Price;
            //res.Category = resCat;
            List<ProductFeatures> ProductFeatures = new List<ProductFeatures>();
            foreach (var item in ProductFeatures)
            {
                ProductFeatures.Add(new ProductFeatures
                {
                    Value = item.Value,
                    DisplayName = item.DisplayName,
                    Product = res,

                });
            }
            _context.ProductFeatures.AddRange(ProductFeatures);
            List<ProductImages> ProductImages = new List<ProductImages>();
            foreach (var item in model.Images)
            {
                //var uploadedResult = UploadFile(item.Src);
                ProductImages.Add(new ProductImages
                {
                    Product = res,
                    //Src = uploadedResult.FileNameAddress,
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
        public UploadDto UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string folder = $@"images\ProductImages\";
                var uploadsRootFolder = Path.Combine(_environment.WebRootPath,folder);
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
