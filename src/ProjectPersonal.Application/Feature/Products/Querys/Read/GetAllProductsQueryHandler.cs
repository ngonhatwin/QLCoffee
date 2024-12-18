using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectPersonal.Application.Common.Exceptions;
using ProjectPersonal.Application.Common.Interfaces.Repository;
using ProjectPersonal.Application.Common.Interfaces.Result;
using ProjectPersonal.Application.Dtos.Response;
using ProjectPersonal.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPersonal.Application.Feature.Products.Querys.Read
{
    internal class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Result<List<ProductResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitofwork<Product> _unitofwork;
        public GetAllProductsQueryHandler(IMapper mapper, IUnitofwork<Product> unitofwork)
        {
            _mapper = mapper;
            _unitofwork = unitofwork;
        }

        public async Task<Result<List<ProductResponse>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var Product = await _unitofwork.GetRepository<Product, Guid>().FindAll().ToListAsync();
                if (Product == null)
                {
                    return new Result<List<ProductResponse>>
                    {
                        Code = (int)Eerrors.Notfound,
                        Message = "NotFound",
                    };
                }
                var result = _mapper.Map<List<ProductResponse>>(Product);
                return new Result<List<ProductResponse>>
                {
                    Code = (int)Eerrors.Success,
                    Message = "Success",
                    Data = result,
                };
            }catch (Exception ex)
            {
                return new Result<List<ProductResponse>>
                {
                    Code = (int)Eerrors.Exceptions,
                    Message = ex.Message,
                };
            }
        }
    }
}
