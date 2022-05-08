using Application.Interfaces.Context;
using Common.Dto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services.Categories.Queries.GetCategories
{
    public class GetCategories : IGetCategories
    {
        private readonly IDataBaseContext _Context;
        public GetCategories(IDataBaseContext context)
        {
            _Context = context;
        }
        ResultDto<List<CategoryDto>> IGetCategories.ExecutResult(long? parentId)
        {
            var category = _Context.Categories
                .Include(p => p.ParentCategory)
                .Include(p => p.SubCategories)
                .Where(p => p.ParentCategoryId == parentId)
                .ToList()
                .Select(p => new CategoryDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Parent = p.ParentCategory != null ? new ParentCategoryDto
                    {
                        Id = p.ParentCategory.Id,
                        Name = p.ParentCategory.Name,
                    } : null,
                    HasChild = p.SubCategories.Count() > 0 ? true : false

                }).ToList();
            return new ResultDto<List<CategoryDto>>()
            {
                Date = category,
                IsSuccess =true,
                Message = "لیست با موفقیت برگشت داده شد."
            };
        }
    }
}
