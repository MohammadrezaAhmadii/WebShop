using Application.Interfaces.Context;
using Common.Dto;
using Domain.Entities.Categories;

namespace Application.Services.Categories.Commands.AddNewCategory
{
    public class AddNewCategory : IAddNewCategory
    {
        private readonly IDataBaseContext _context;
        public AddNewCategory(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto ExecutResult(long? parentId, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "لطفا نام دسته بندی را وارد کنید!",
                };
            }
            Category category = new Category()
            {
                Name = name,
                ParentCategory = GetParent(parentId)
            };
            _context.Categories.Add(category);
            _context.SaveChanges();
            return new ResultDto
            {
                IsSuccess = true,
                Message = "با موفقیت انجام شد."
            };

           
        }
        public Category GetParent(long? parentId)
        {
            return _context.Categories.Find(parentId);
    }
    }
}
