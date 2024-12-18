using BloomFilter;
using BloomFilter.Configurations;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ProjectPersonal.Application.Common.Interfaces.Repository;
using ProjectPersonal.Application.Feature.Products.Commands.Create;
using ProjectPersonal.Infrastructure.Repository;
using System.Reflection;


namespace ProjectPersonal.Infrastructure
{
    //Cấu hình DI, Redis,...
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services)
        {
            var options = new BloomFilter.Configurations.BloomFilterOptions();
            return services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                .AddScoped(typeof(IUnitofwork<>), typeof(Unitofwork<>))
                .AddScoped(typeof(IRepositoryBaseAsync<,>), typeof(RepositoryBaseAsync<,>))
                .AddScoped<IJwtRepository, JwtRepository>()
                .AddBloomFilter(options =>
                {
                    options.UseInMemory();
                })
                .AddMediatR(cfg =>
                            cfg.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly))
            ;
        }
    }
}
