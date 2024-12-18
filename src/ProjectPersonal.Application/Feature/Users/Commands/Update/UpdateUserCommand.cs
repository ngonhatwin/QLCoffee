using MediatR;
using ProjectPersonal.Application.Common.Interfaces.Result;
using ProjectPersonal.Application.Dtos.Request;
using ProjectPersonal.Application.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPersonal.Application.Feature.Users.Commands.Update
{
    public class UpdateUserCommand : IRequest<Result<UserResponse>>
    {
        public Guid Id { get; set; }
        public UserRequest UserRequest { get; set; }
    }
}
