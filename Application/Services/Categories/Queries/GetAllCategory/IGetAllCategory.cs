using Common.Dto;
using Domain.Entities.Categories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Categories.Queries.GetAllCategory
{
    public interface IGetAllCategory
    {
        ResultDto<List<GetAllCategoryDto>> Result();
    }
}
