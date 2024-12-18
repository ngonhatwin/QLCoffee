using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectPersonal.Application.Common.Interfaces.Repository;
using ProjectPersonal.Application.Common.Interfaces.Result;
using ProjectPersonal.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPersonal.Application.Feature.Users.Commands.Delete
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<Guid>>
    {
        public readonly IMapper _mapper;
        public readonly IUnitofwork<User> _unitOfWork;
        public async Task<Result<Guid>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _unitOfWork.GetRepository<User, Guid>()
                    .FindByCondition(x => x.Id == request.Id)
                    .FirstOrDefaultAsync();
                if (user == null)
                {
                    return new Result<Guid>
                    {
                        Code = (int)Eerrors.Notfound,
                        Message = "Not found",
                        Data = request.Id,
                    };
                }
                await _unitOfWork.GetRepository<User, Guid>().DeleteAsync(user);
                await _unitOfWork.GetRepository<User, Guid>().SaveChangesAsync();
                return new Result<Guid>
                {
                    Code = (int)Eerrors.Success,
                    Message = "Success",
                    Data = user.Id,
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
