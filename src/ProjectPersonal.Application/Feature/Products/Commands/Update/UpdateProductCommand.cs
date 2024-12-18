using MediatR;
using ProjectPersonal.Application.Common.Interfaces.Result;
using ProjectPersonal.Application.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPersonal.Application.Feature.Products.Commands.Update
{
    public class UpdateProductCommand : IRequest<Result<Product>>
    {
        public Guid Id { get; set; }
        public ProductRequest ProductRequest { get; set; }
    }
}
