using MediatR;
using ProjectPersonal.Application.Common.Interfaces.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPersonal.Application.Feature.Category.Commands.Create
{
    public class CreateCategoryCommand : IRequest<Result<Guid>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
