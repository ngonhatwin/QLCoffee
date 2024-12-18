using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectPersonal.Application.Common.Interfaces.Repository;
using ProjectPersonal.Application.Common.Interfaces.Result;
using ProjectPersonal.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPersonal.Application.Feature.Products.Querys.Read
{
    public class CountProductsQueryHandler : IRequestHandler<CountProductsQuery, Result<int>>
    {
        private readonly IUnitofwork<Product> _unitofwork;
        public CountProductsQueryHandler(IUnitofwork<Product> unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public async Task<Result<int>> Handle(CountProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _unitofwork.GetRepository<Product, Guid>().FindAll().ToListAsync();
            var countProduct = products.Count();
            if(countProduct <= 0)
            {
                return new Result<int>
                {
                    Code = (int)Eerrors.Notfound,
                    Message = "Not found",
                };
            }
            return new Result<int>
            {
                Code = (int)Eerrors.Success,
                Message = "Success",
                Data = countProduct
            };
        }
    }
}
