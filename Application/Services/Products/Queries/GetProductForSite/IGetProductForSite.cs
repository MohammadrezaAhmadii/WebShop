using Application.Interfaces.Context;
using Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Application.Services.Products.Queries.GetProductForSite
{
    public interface IGetProductForSite
    {
        ResultDto<GetProductForSiteDto> ResultDto(string searchkey, int page, long? Id);
    }
    public class GetProductForSite : IGetProductForSite
    {
        private readonly IDataBaseContext _context;
        public GetProductForSite(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<GetProductForSiteDto> ResultDto(string searchkey, int page, long? Id)
        {
            int totalRow = 0;
            var productQuery = _context.Products
                .Include(p => p.ProductImages).AsQueryable();

            if (Id != null)
            {
                productQuery = productQuery.Where(p => p.CategoryId == Id || p.Category.ParentCategoryId == Id).AsQueryable();
            }
            if (!string.IsNullOrWhiteSpace(searchkey))
            {
                productQuery = productQuery.Where(p => p.Name.Contains(searchkey) || p.Brand.Contains(searchkey)).AsQueryable();
            }
            var product = productQuery.ToPage(page, 5, out totalRow);
            Random rd = new Random();
            return new ResultDto<GetProductForSiteDto>
            {
                Date = new GetProductForSiteDto
                {
                    TotalRow = totalRow,
                    Products = product.Select(p => new ProductForSiteDto
                    {
                        Id = p.Id,
                        Star = rd.Next(1, 5),
                        Title = p.Name,
                        ImageSrc = p.ProductImages.FirstOrDefault().Src,
                        Price = p.Price
                    }).ToList(),
                },
                IsSuccess = true,
            };
        }
    }
    public class GetProductForSiteDto
    {
        public List<ProductForSiteDto> Products { get; set; }
        public int TotalRow { get; set; }
    }

    public class ProductForSiteDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string ImageSrc { get; set; }
        public int Star { get; set; }
        public int Price { get; set; }
    }
}
