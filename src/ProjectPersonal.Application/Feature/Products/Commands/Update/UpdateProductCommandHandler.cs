using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectPersonal.Application.Common.Interfaces.Repository;
using ProjectPersonal.Application.Common.Interfaces.Result;
using ProjectPersonal.Application.Dtos.Response;
using ProjectPersonal.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPersonal.Application.Feature.Products.Commands.Update
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<Product>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitofwork<Product> _unitOfWork;
        public UpdateProductCommandHandler(IMapper mapper, IUnitofwork<Product> unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Product>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
               var productEntity = await _unitOfWork.GetRepository<Product, Guid>()
              .FindByCondition(x => x.Id == request.Id)
              .SingleOrDefaultAsync();
                if (productEntity == null)
                {
                    return new Result<Product>
                    {
                        Code = (int)Eerrors.Notfound,
                        Message = "Not found",
                    };
                }
                productEntity.Name = request.ProductRequest.Name;
                productEntity.Price = request.ProductRequest.Price;
                productEntity.Description = request.ProductRequest.Description;
                productEntity.Stock = request.ProductRequest.Stock;
                productEntity.ModifiedDate = DateTime.UtcNow;
                productEntity.ModifiedBy = "SystemNew";
                await _unitOfWork.GetRepository<Product, Guid>().UpdateAsync(request.Id, productEntity);
                await _unitOfWork.GetRepository<Product, Guid>().SaveChangesAsync();
                _mapper.Map<ProductResponse>(productEntity);
                return new Result<Product>
                {
                    Code = (int)Eerrors.Success,
                    Message = "Update Success",
                    Data = productEntity,
                };
            }catch (Exception ex)
            {
                return new Result<Product>
                {
                    Code = (int)Eerrors.Exceptions,
                    Message = ex.Message,
                };
            }
          
        }
    }
}
