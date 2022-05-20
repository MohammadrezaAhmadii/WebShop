using Application.Interfaces.Context;
using Common.Dto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services.Categories.Queries.GetAllCategory
{
    public class GetAllCategory : IGetAllCategory
    {
        private readonly IDataBaseContext _context;


        public GetAllCategory(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<GetAllCategoryDto>> Result()
        {
            var Categories = _context.Categories
                .Where(p => p.ParentCategoryId != null)
                .ToList()
                .Select(p => new GetAllCategoryDto
                {
                    Id = p.Id,
                    Name = p.Name
                }).ToList();
            return new ResultDto<List<GetAllCategoryDto>>()
            {
                Date = Categories,
                IsSuccess = true,

            };
        }
    }
}
