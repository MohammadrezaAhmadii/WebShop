using Application.Interfaces.Context;
using Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Categories.Queries.GetMenuItem
{
    public interface IGetMenuItem
    {
        ResultDto<List<MenuItemDto>> ResultDto();
    }
    public class GetMenuItem : IGetMenuItem
    {
        private readonly IDataBaseContext _context;
        public GetMenuItem(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<MenuItemDto>> ResultDto()
        {
            var menuItem = _context.Categories
                .Include(p => p.SubCategories)
                .Where(p => p.ParentCategoryId == null)
                .ToList()
                .Select(p => new MenuItemDto
                {
                    catId=p.Id,
                    Name = p.Name,
                    child = p.SubCategories.ToList().Select(p=>new MenuItemDto
                    {
                        catId = p.Id,
                        Name = p.Name
                    }).ToList(),
                }).ToList();
            return new ResultDto<List<MenuItemDto>>
            {
                Date = menuItem,
                IsSuccess = true,
            };

        }
    }
    public class MenuItemDto
    {
        public long? catId { get; set;}
        public string Name { get; set; }
        public List<MenuItemDto> child { get; set; }
    }
}
