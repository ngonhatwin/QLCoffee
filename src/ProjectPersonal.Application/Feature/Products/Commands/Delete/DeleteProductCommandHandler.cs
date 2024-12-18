using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectPersonal.Application.Common.Interfaces.Repository;
using ProjectPersonal.Application.Common.Interfaces.Result;
using ProjectPersonal.Domain.Enum;

namespace ProjectPersonal.Application.Feature.Products.Commands.Delete
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<Guid>>
    {
        private readonly IUnitofwork<Product> _unitOfWork;
        public DeleteProductCommandHandler(IMapper mapper, IUnitofwork<Product> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var productRepository = await _unitOfWork.GetRepository<Product, Guid>()
                 .FindByCondition(x => x.Id == request.Id)
                 .SingleOrDefaultAsync();
                if (productRepository == null)
                {
                    return new Result<Guid>
                    {
                        Code = (int)Eerrors.Notfound,
                        Message = "Not Found",
                    };
                }
                await _unitOfWork.GetRepository<Product, Guid>().DeleteAsync(productRepository);
                await _unitOfWork.GetRepository<Product, Guid>().SaveChangesAsync();
                return new Result<Guid>
                {
                    Code = (int)Eerrors.Success,
                    Message = "Success",
                    Data = productRepository.Id,
                };
            }
            catch (Exception ex)
            {
                return new Result<Guid>
                {
                    Code = (int)Eerrors.Exceptions,
                    Message = ex.Message,
                };  
            }
        }
    }
}
