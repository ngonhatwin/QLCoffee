using MediatR;
using ProjectPersonal.Application.Common.Interfaces.Result;
using ProjectPersonal.Application.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPersonal.Application.Feature.LoginValid.Commands
{
    public class EmailExitsValidCommand : IRequest<Result<EmailEsxitsResponse>>
    {
        public string Email { get; set; }
    }
}
