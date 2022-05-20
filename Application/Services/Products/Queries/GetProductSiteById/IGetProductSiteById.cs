using Application.Interfaces.Context;
using Common.Dto;
using Domain.Entities.Categories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Products.Queries.GetProductSiteById
{
    public interface IGetProductSiteById
    {
        ResultDto<ProductSiteDto> resultDto(long Id);
    }
    public class GetProductSiteById : IGetProductSiteById
    {
        private readonly IDataBaseContext _context;
        public GetProductSiteById(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ProductSiteDto> resultDto(long Id)
        {
            try
            {
                if(Id == 0)
                {
                    return new ResultDto<ProductSiteDto>
                    {
                        IsSuccess = false,
                        Message = "محصولی یافت نشد!",
                    };
                }
                var GetById = _context.Products
               .Include(p => p.Category)
               .ThenInclude(p => p.ParentCategory)
               .Include(p => p.ProductFeatures)
               .Include(p => p.ProductImages)
               .Where(p => p.Id == Id)
               .FirstOrDefault();
                //var GetById = _context.Products.Where(a => a.Id == Id).FirstOrDefault();
                return new ResultDto<ProductSiteDto>
                {
                    Date = new ProductSiteDto
                    {
                        Id = GetById.Id,
                        Brand = GetById.Brand,
                        //Name = GetById.Name,
                        //Display = GetById.Displayed,
                        //Inventory = GetById.Inventory,
                        Model = GetById.Model,
                        Description = GetById.Description,
                        Category = GetCategory(GetById.Category),
                        Feachers = GetById.ProductFeatures.ToList().Select(p => new ProductFeacherSiteDto
                        {
                            DisplayName = p.DisplayName,
                            Value = p.Value,
                            Id = p.Id,
                        }).ToList(),
                        Images = GetById.ProductImages.Select(p => p.Src).ToList(),
                    },
                    IsSuccess = true,
                    Message = ""
                };


            }
            catch
            {
                return new ResultDto<ProductSiteDto>
                {
                    IsSuccess = false,
                    Message = "محصولی یافت نشد!",
                };
            }
        }
            private string GetCategory(Category category)
            {
                string result = $"{category.ParentCategory.Name} - {category.ParentCategory.Name}";
                return result += category.Name;
            }
    }
    public class ProductSiteDto
    {
        public string Model { get; set; }
        public long Id { get; set; }
        public string Title { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        //public List<string> Images { get; set; }
        public List<ProductFeacherSiteDto> Feachers { get; set; }
        public List<string> Images { get; set; }

    }
    public class ProductFeacherSiteDto
    {
        public long Id { get; set; }
        public string DisplayName { get; set; }
        public string Value { get; set; }
    }
    //public class ProductImageSiteDto
    //{
    //    public long Id { get; set; }
    //    public string Src { get; set; }
    //}
}
