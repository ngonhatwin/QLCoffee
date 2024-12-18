using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using ProjectPersonal.Application.Common.Interfaces.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPersonal.Application.Feature.Users.Commands.Delete
{
    public class DeleteUserCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }
}
