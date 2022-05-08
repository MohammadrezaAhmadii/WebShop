using Application.Interfaces.Context;
using Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Application.Services.Products.Queries.GetProductAdmin
{
    public interface IGetProductAdmin
    {
        ResultDto<ProductAdminDto> Execute(int Page=1, int PageSize=20);
    }
    public class GetProductAdmin : IGetProductAdmin
    {
        private readonly IDataBaseContext _context;
        public GetProductAdmin(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ProductAdminDto> Execute(int Page = 1, int PageSize = 20)
        {
            int rowCount = 0;
            var Product = _context.Products
                .Include(p => p.Category)
                .ToPage(Page, PageSize, out rowCount)
                .Select(p => new ProductsAdminDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Brand = p.Brand,
                    Description = p.Description,
                    Model = p.Model,
                    Displayed = p.Displayed,
                    Inventory = p.Inventory,
                    Price = p.Price,
                    Category = p.Category.Name,
                }).ToList();
            return new ResultDto<ProductAdminDto>
            {
                Date = new ProductAdminDto
                {
                    CurrentPage = Page,
                    PageSize = PageSize,
                    Products = Product,
                    RowCount = rowCount
                },
                IsSuccess = true,
                Message = ""

            };
        }
    }
    public class ProductAdminDto
    {
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        public List<ProductsAdminDto> Products { get; set; }
    }

    public class ProductsAdminDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Inventory { get; set; }
        public bool Displayed { get; set; }
    }
}
