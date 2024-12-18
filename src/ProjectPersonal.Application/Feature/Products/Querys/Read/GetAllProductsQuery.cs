using MediatR;
using ProjectPersonal.Application.Common.Interfaces.Result;
using ProjectPersonal.Application.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPersonal.Application.Feature.Products.Querys.Read
{
    public class GetAllProductsQuery : IRequest<Result<List<ProductResponse>>>
    {

    }
}
