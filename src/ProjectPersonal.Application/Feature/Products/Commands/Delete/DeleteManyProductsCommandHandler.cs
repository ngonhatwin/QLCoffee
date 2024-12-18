using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectPersonal.Application.Common.Interfaces.Repository;
using ProjectPersonal.Application.Common.Interfaces.Result;
using ProjectPersonal.Domain.Enum;

namespace ProjectPersonal.Application.Feature.Products.Commands.Delete
{
    public class DeleteManyProductsCommandHandler : IRequestHandler<DeleteManyProductsCommand, Result<string>>
    {
        private readonly IUnitofwork<Product> _unitOfWork;

        public DeleteManyProductsCommandHandler(IUnitofwork<Product> unitofwork)
        {
            _unitOfWork = unitofwork;
        }

        public async Task<Result<string>> Handle(DeleteManyProductsCommand request, CancellationToken cancellationToken)
        {
            var productsIdRequestCount = request.ProductIds.Count();
            var products = await _unitOfWork.GetRepository<Product, Guid>().FindAll().
                Where(x => request.ProductIds.Contains(x.Id)).ToListAsync();     
            if (productsIdRequestCount != products.Count)
            {
                return new Result<string>
                {
                    Code = (int)Eerrors.Notfound,
                    Message = "No data available",
                };
            }    
            await _unitOfWork.GetRepository<Product, Guid>().DeleteManyAsync(products);
            await _unitOfWork.GetRepository<Product, Guid>().SaveChangesAsync();
            return new Result<string>
            {
                Code = (int)Eerrors.Success,
                Message = "Success"
            };
        }
    }
}
