using MediatR;
using ProjectPersonal.Application.Common.Interfaces.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPersonal.Application.Feature.Products.Querys.Read
{
    public class CountProductsQuery : IRequest<Result<int>>
    {

    }
}
