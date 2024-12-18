using AutoMapper;
using ProjectPersonal.Application.Dtos.Response;
using ProjectPersonal.Application.Feature.Category.Commands.Create;
using ProjectPersonal.Application.Feature.Products.Commands.Create;
using ProjectPersonal.Application.Feature.Users.Commands.Create;
using ProjectPersonal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPersonal.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
            CreateMap<CreateProductCommand, Product>().ReverseMap();
            CreateMap<ProductResponse, Product>().ReverseMap();
            CreateMap<CreateUserCommand, User>().ReverseMap();
            CreateMap<UserResponse, User>().ReverseMap();
            CreateMap<CreateCategoryCommand, Categories>().ReverseMap();
            CreateMap<CategoryResponse, Categories>().ReverseMap();
        }
        //private void ApplyMappingsFromAssembly(Assembly assembly)
        //{
        //    var mapFromType = typeof(IMapFrom<>);
        //    const string mappingMethodName = nameof(IMapFrom<object>.Mapping);

        //    var types = assembly.GetExportedTypes()
        //        .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == mapFromType))
        //        .ToList();

        //    foreach (var type in types)
        //    {
        //        var instance = Activator.CreateInstance(type);
        //        if (instance == null) continue;

        //        var methodInfo = type.GetMethod(mappingMethodName) ??
        //                         type.GetInterfaces()
        //                             .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == mapFromType)?
        //                             .GetMethod(mappingMethodName);

        //        methodInfo?.Invoke(instance, new object[] { this });
        //    }
        //}

    }
}
