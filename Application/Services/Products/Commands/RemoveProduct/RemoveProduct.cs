using Application.Interfaces.Context;
using Common.Dto;
using Domain.Entities.Products;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Application.Services.Products.Commands
{
    public class RemoveProduct : IRemoveProduct
    {
        private readonly IDataBaseContext _context;

        public RemoveProduct(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto ExecutResult(long productId)
        {
            try
            {
                if (productId == 0) 
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "عملیات با خطا مواجه گردید"
                    };
                }

                Product product = _context.Products.FirstOrDefault(x => x.Id == productId);
                product.IsRemoved = true;
                _context.SaveChanges();

                return new ResultDto
                {
                    IsSuccess = true,
                    Message = "با موفقیت حذف گردید.",
                };
            }
            catch
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "عملیات با خطا مواجه گردید"
                };
            }
        }
    }
}