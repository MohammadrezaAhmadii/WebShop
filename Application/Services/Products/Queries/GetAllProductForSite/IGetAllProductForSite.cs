using Application.Interfaces.Context;
using Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Application.Services.Products.Queries.GetAllProductForSite
{
    public interface IGetAllProductForSite
    {
        ResultDto<ResultProductSiteDto> Execute(string Searchkey, long? catId = null, int page = 1);
    }
    public class GetAllProductForSite : IGetAllProductForSite
    {
        private readonly IDataBaseContext _context;
        public GetAllProductForSite(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultProductSiteDto> Execute(string Searchkey, long? catId = null, int page = 1)
        {
            int totalRow = 0;
            var productQuery = _context.Products.Include(p => p.ProductImages).AsQueryable();
            if(catId != null)
            {
                productQuery = productQuery.Where(p => p.CategoryId == catId || p.Category.ParentCategoryId == catId);
            }
            if (string.IsNullOrWhiteSpace(Searchkey))
            {
                productQuery = productQuery.Where(p => p.Name.Contains(Searchkey) || p.Brand.Contains(Searchkey)).AsQueryable();
            }
            var product = productQuery.ToPage(page,5, out totalRow);
            Random rd = new Random();
            return new ResultDto<ResultProductSiteDto>
            {
                Date = new ResultProductSiteDto
                {
                    Products = product.Select(p => new ProductForSiteDto
                    {
                        Id = p.Id,
                        Star = rd.Next(1, 5),
                        Title = p.Name,
                        Price = p.Price,
                        ImageSrc = p.ProductImages.FirstOrDefault().Src,
                    }).ToList(),
                },
                IsSuccess = true,
            };
        }
    }
    public class ResultProductSiteDto
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
