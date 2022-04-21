using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Errrors;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
             services.AddScoped<IProductRepository,ProductRepository>();
             services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped(typeof(IGenericRepository<>),(typeof(GenericRepository<>)));
              services.Configure<ApiBehaviorOptions>(options =>
             {options.InvalidModelStateResponseFactory = actionContext =>
             {
                 var errors = actionContext.ModelState
                 .Where(e => e.Value.Errors.Count > 0)
                 .SelectMany(x => x.Value.Errors)
                 .Select(x => x.ErrorMessage).ToArray();
                 

                 var errorResponce = new ApiValidationErrorResponce
                 {
                     Errors =errors
                 };
                 return new BadRequestObjectResult(errorResponce);
             };
             });
             return services;
        }
        
    }
}