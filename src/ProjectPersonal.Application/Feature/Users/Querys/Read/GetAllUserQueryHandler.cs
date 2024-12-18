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

namespace ProjectPersonal.Application.Feature.Users.Querys.Read
{
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, Result<List<UserResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitofwork<User> _unitOfWork;
        public GetAllUserQueryHandler(IMapper mapper, IUnitofwork<User> unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<List<UserResponse>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _unitOfWork.GetRepository<User, UserResponse>().FindAll().ToListAsync();
                if (user == null)
                {
                    return new Result<List<UserResponse>>
                    {
                        Code = (int)Eerrors.Notfound,
                        Message = "Not found",
                    };
                }
                var userResponse = _mapper.Map<List<UserResponse>>(user);
                return new Result<List<UserResponse>>
                {
                    Code = (int)Eerrors.Success,
                    Message = "Success",
                    Data = userResponse
                };
            }
            catch (Exception ex)
            {
                return new Result<List<UserResponse>>
                {
                    Code = (int)Eerrors.Exceptions,
                    Message = ex.Message,
                };
            }
        }
    }
}
