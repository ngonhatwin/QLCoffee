using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectPersonal.Application.Common.Interfaces.Repository;
using ProjectPersonal.Application.Common.Interfaces.Result;
using ProjectPersonal.Application.Dtos.Response;
using ProjectPersonal.Domain.Entities;
using ProjectPersonal.Domain.Enum;

namespace ProjectPersonal.Application.Feature.Category.Querys.Read
{
    public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, Result<List<CategoryResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitofwork<Categories> _unitOfWork;

        public GetAllCategoryQueryHandler(IMapper mapper, IUnitofwork<Categories> unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<CategoryResponse>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var category = await _unitOfWork.GetRepository<Categories, Guid>().FindAll().ToListAsync();
                if (category == null)
                {
                    return new Result<List<CategoryResponse>>
                    {
                        Code = (int)Eerrors.Notfound,
                        Message = "NotFound",
                    };
                }
                var categoryRespon = _mapper.Map<List<CategoryResponse>>(category);
                return new Result<List<CategoryResponse>>
                {
                    Code = (int)Eerrors.Success,
                    Message = "Success",
                    Data = categoryRespon,
                };
            }
            catch (Exception ex)
            {
                return new Result<List<CategoryResponse>>
                {
                    Code = (int)Eerrors.Exceptions,
                    Message = ex.Message,
                };
            } 
        }
    }
}
