using Application.Interfaces.Context;
using Common.Dto;
using Domain.Entities.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Categories.Commands.EditCategory
{
    public interface IEditCategory
    {
        ResultDto resultDto(long parentId, string name);
    }
    public class EditCategory : IEditCategory 
    {
        private readonly IDataBaseContext _context;
        public EditCategory(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto resultDto(long parentId, string name)
        {
            var res = _context.Categories.FirstOrDefault(r => r.Id == parentId);
            res.Name = name;
            res.UpdateDateTime = DateTime.UtcNow;
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "باموفقیت تغییرات انجام شد"
            };
        }
    }
}
