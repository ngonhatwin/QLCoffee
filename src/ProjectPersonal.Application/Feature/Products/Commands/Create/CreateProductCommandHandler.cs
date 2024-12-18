using AutoMapper;
using MediatR;
using ProjectPersonal.Application.Common.Interfaces.Repository;
using ProjectPersonal.Application.Common.Interfaces.Result;
using ProjectPersonal.Application.Feature.Products.Validator;
using ProjectPersonal;
using ProjectPersonal.Application.Feature.Products.Commands.Create;
using ProjectPersonal.Domain.Enum;
public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<Guid>>
{
    private readonly IUnitofwork<Product> _unitOfWork;
    private readonly IMapper _mapper;
    public CreateProductCommandHandler(IUnitofwork<Product> unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var validator = new CreateProductCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new Result<Guid>
                {
                    Code = (int)Eerrors.ValidExceptions,
                    Data = default,
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }
            var product = _mapper.Map<Product>(request);
            product.CreatedDate = DateTime.UtcNow;
            product.CreatedBy = "System";
            await _unitOfWork.GetRepository<Product, Guid>().CreateAsync(product);
            await _unitOfWork.GetRepository<Product, Guid>().SaveChangesAsync();
            return new Result<Guid>
            {
                Code = (int)Eerrors.Success,
                Data = product.Id,
                Message = "Success"
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
