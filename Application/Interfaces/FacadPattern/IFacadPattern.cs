using Application.Services.Categories.Commands.AddNewCategory;
using Application.Services.Categories.Queries.GetAllCategory;
using Application.Services.Categories.Queries.GetCategories;
using Application.Services.Products.Commands;
using Application.Services.Products.Queries.GetProductAdmin;
using Application.Services.Products.Queries.GetProductDetailAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.FacadPattern
{
    public interface IFacadPattern
    {
        AddNewCategory AddNewCategory { get; }
        AddNewProduct AddNewProduct { get; }
        IGetCategories GetCategories { get; }
        IGetAllCategory GetAllCategory { get; }
        IGetProductDetailAdmin GetProductDetailAdmin { get; }
        IGetProductAdmin GetProductAdmin { get; }


    }
}
