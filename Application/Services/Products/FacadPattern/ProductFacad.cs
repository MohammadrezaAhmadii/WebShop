using Application.Interfaces.Context;
using Application.Interfaces.FacadPattern;
using Application.Services.Categories.Commands.AddNewCategory;
using Application.Services.Categories.Queries.GetAllCategory;
using Application.Services.Categories.Queries.GetCategories;
using Application.Services.Products.Commands;
using Application.Services.Products.Queries.GetAllProductForSite;
using Application.Services.Products.Queries.GetProductAdmin;
using Application.Services.Products.Queries.GetProductDetailAdmin;
using Application.Services.Products.Queries.GetProductForSite;
using Application.Services.Products.Queries.GetProductSiteById;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Categories.FacadPattern
{
    public class ProductFacad: IFacadPattern
    {
        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _environment;
        public ProductFacad(IDataBaseContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _environment = hostingEnvironment;

        }
        private AddNewCategory _addNewCategory;
        public AddNewCategory AddNewCategory
        {
            get
            {
                return _addNewCategory = _addNewCategory ?? new AddNewCategory(_context);
            }
        }



        private AddNewProduct _addNewProduct;
        public AddNewProduct AddNewProduct
        {
            get
            {
                return _addNewProduct = _addNewProduct ?? new AddNewProduct(_context, _environment);
            }
        }

        private RemoveProduct _removeProduct;
        public RemoveProduct RemoveProduct
        {
            get
            {
                return _removeProduct = _removeProduct ?? new RemoveProduct(_context);
            }
        }

        private IGetCategories _getCategories;
        public IGetCategories GetCategories
        {
            get
            {
                return _getCategories = _getCategories ?? new GetCategories(_context);
            }
        }
        private IGetAllCategory _getAllCategory;
        public IGetAllCategory GetAllCategory
        {
            get
            {
                return _getAllCategory = _getAllCategory ?? new GetAllCategory(_context);
            }
        }
        private IGetProductDetailAdmin _getProductDetailAdmin;
        public IGetProductDetailAdmin GetProductDetailAdmin 
        {
            get
            {
                return _getProductDetailAdmin = _getProductDetailAdmin ?? new GetProductDetailAdmin(_context);
            }
        }
        private IGetProductAdmin _getProductAdmin;
        public IGetProductAdmin GetProductAdmin 
        {
            get
            {
                return _getProductAdmin = _getProductAdmin ?? new GetProductAdmin(_context);
            }
        }
        private IGetProductSiteById _getProductSiteById;
        public IGetProductSiteById GetProductSiteById 
        {
            get
            {
                return _getProductSiteById = _getProductSiteById ?? new GetProductSiteById(_context);
            }
        }
        private IGetAllProductForSite _getAllProductForSite;
        public IGetAllProductForSite GetAllProductForSite 
        {
            get
            {
                return _getAllProductForSite = _getAllProductForSite ?? new GetAllProductForSite(_context);
            }
        }
        private IGetProductForSite _getProductForSite;
        public IGetProductForSite GetProductForSite 
        {
            get
            {
                return _getProductForSite = _getProductForSite ?? new GetProductForSite(_context);
            }
        }
    }
}
