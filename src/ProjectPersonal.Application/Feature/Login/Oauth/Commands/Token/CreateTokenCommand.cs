using MediatR;
using ProjectPersonal.Application.Common.Interfaces.Result;
using ProjectPersonal.Application.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPersonal.Application.Feature.Login.Oauth.Commands.Token
{
    public class CreateTokenCommand : IRequest<Result<AuthenResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
