using AutoMapper;
using MediatR;
using ProjectPersonal.Application.Common.Interfaces.Result;
using ProjectPersonal.Application.Common.Mappings;
using ProjectPersonal.Application.Dtos.Request;

namespace ProjectPersonal.Application.Feature.Products.Commands.Create
{
    public class CreateProductCommand : IRequest<Result<Guid>>
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int? Stock { get; set; }
        public Guid CategoryID { get; set; }
        public string ImageURL { get; set; }
    }
}
