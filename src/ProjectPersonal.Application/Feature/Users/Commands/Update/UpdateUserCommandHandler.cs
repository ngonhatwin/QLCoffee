using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectPersonal.Application.Common.Interfaces.Repository;
using ProjectPersonal.Application.Common.Interfaces.Result;
using ProjectPersonal.Application.Dtos.Response;
using ProjectPersonal.Application.Feature.Users.Commands.Delete;
using ProjectPersonal.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPersonal.Application.Feature.Users.Commands.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<UserResponse>>
    {
        private readonly IUnitofwork<User> _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateUserCommandHandler(Common.Interfaces.Repository.IUnitofwork<User> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<UserResponse>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.GetRepository<User, Guid>()
                .FindByCondition(x => x.Id == request.Id)
                .FirstOrDefaultAsync();
            if (user == null)
            {
                return new Result<UserResponse>
                {
                    Code = (int)Eerrors.Notfound,
                    Message = "Not found"
                };
            }
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.UserRequest.Password, BCrypt.Net.BCrypt.GenerateSalt());
            user.Address = request.UserRequest.Address;
            user.Password = passwordHash;
            user.Email = request.UserRequest.Email;
            user.FullName = request.UserRequest.FullName;
            user.ModifiedDate = DateTime.UtcNow;
            await _unitOfWork.GetRepository<User, Guid>().UpdateAsync(user.Id, user);
            await _unitOfWork.GetRepository<User, Guid>().SaveChangesAsync();
            var userResponse = _mapper.Map<UserResponse>(user);
            return new Result<UserResponse>
            {
                Code = (int)Eerrors.Success,
                Message = "Success",
                Data = userResponse,
            };
        }
    }
}
