using Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Categories.Commands.AddNewCategory
{
    public interface IAddNewCategory
    {
        ResultDto ExecutResult(long? parentId, string name);
    }
}
