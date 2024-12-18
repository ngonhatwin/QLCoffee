using AutoMapper;
using MediatR;
using ProjectPersonal.Application.Common.Interfaces.Repository;
using ProjectPersonal.Application.Common.Interfaces.Result;
using ProjectPersonal.Application.Feature.Category.Validation;
using ProjectPersonal.Domain.Entities;
using ProjectPersonal.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPersonal.Application.Feature.Category.Commands.Create
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result<Guid>>
    {
        private readonly IUnitofwork<Categories> _unitofwork;
        private readonly IMapper _mapper;
        public CreateCategoryCommandHandler(IUnitofwork<Categories> unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validator = new CreateCategoryCommandValidator();
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
                var category = _mapper.Map<Categories>(request);
                await _unitofwork.GetRepository<Categories, Guid>().CreateAsync(category);
                await _unitofwork.GetRepository<Categories, Guid>().SaveChangesAsync();
                return new Result<Guid>
                {
                    Code = (int)Eerrors.Success,
                    Data = category.Id,
                    Message = "Success",
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
