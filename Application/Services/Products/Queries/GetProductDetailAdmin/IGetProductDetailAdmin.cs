using Application.Interfaces.Context;
using Common.Dto;
using Domain.Entities.Categories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Products.Queries.GetProductDetailAdmin
{
    public interface IGetProductDetailAdmin
    {
        ResultDto <ProductDetailDto> Execut(long Id);
    }
    public class GetProductDetailAdmin : IGetProductDetailAdmin
    {
        private readonly IDataBaseContext _context;
        public GetProductDetailAdmin(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto <ProductDetailDto> Execut(long Id)
        {
            var product = _context.Products
                .Include(p => p.Category)
                .ThenInclude(p => p.ParentCategory)
                .Include(p => p.ProductFeatures)
                .Include(p => p.ProductImages)
                .Where(p => p.Id == Id)
                .FirstOrDefault();
            return new ResultDto<ProductDetailDto>
            {
                Date = new ProductDetailDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Model = product.Model,
                    Brand = product.Brand,
                    Category = GetCategory(product.Category),
                    Description = product.Description,
                    Display = product.Displayed,
                    Inventory = product.Inventory,
                    Feachers = product.ProductFeatures.ToList().Select(p => new ProductFeacherDto
                    {
                        DisplayName = p.DisplayName,
                        Value = p.Value,
                        Id = p.Id,
                    }).ToList(),
                    Images = product.ProductImages.ToList().Select(p => new ProductImageDto
                    {
                        Id = p.Id,
                        Src = p.Src
                    }).ToList()
                },
                IsSuccess = true,
                Message = "",
            };
        }
            private string GetCategory(Category category)
            {
                string result = category.ParentCategory != null ? $"{category.ParentCategory.Name} - " : "";
                return result += category.Name;
            }

    }
    public class ProductDetailDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public bool Display { get; set; }
        public int Inventory { get; set; }
        public string Description { get; set; } 
        public List<ProductFeacherDto> Feachers { get; set; }
        public List<ProductImageDto> Images { get; set; }

    }
    public class ProductFeacherDto
    {
        public long Id { get; set; }
        public string DisplayName { get; set; }
        public string Value { get; set; }
    }
    public class ProductImageDto
    {
        public long Id { get; set; }
        public string Src { get; set; }
    }
}
