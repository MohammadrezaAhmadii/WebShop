using Application.Interfaces.Context;
using Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Categories.Commands.RemoveCategory
{
    public interface IRemoveCategory
    {
        ResultDto ExecutResult(long id);
    }
    public class RemoveCategory : IRemoveCategory
    {
        private readonly IDataBaseContext _context;
        public RemoveCategory(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto ExecutResult(long id)
        {

            var category = _context.Categories.FirstOrDefault(p => p.Id == id);
            if (category == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "عملیات با خطا مواجه گردید"
                };
            }
            category.RemoveDateTime = DateTime.Now;
            category.IsRemoved = true;
            _context.SaveChanges();
            return new ResultDto
            {
                IsSuccess = true,
                Message = "باموفقیت حذف گردید!"
            };
        }
    }
}
