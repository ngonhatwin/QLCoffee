using MediatR;
using ProjectPersonal.Application.Common.Interfaces.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPersonal.Application.Feature.Products.Commands.Delete
{
    public class DeleteManyProductsCommand : IRequest<Result<string>>
    {
        public List<Guid> ProductIds { get; set; }
    }
}
