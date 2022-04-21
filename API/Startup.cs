using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
//using API.Data;

using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Interfaces;
using AutoMapper;
using API.Helpers;
using API.Middleware;
using API.Errrors;
using API.Extensions;

namespace API
{
    public class Startup
    {

       
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

     

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddControllers();
            services.AddDbContext<StoreContext>(options => 
            options.UseNpgsql(_config.GetConnectionString("DefaultConnection")));
            services.AddApplicationServices();
            services.AddSwaggerDocumentation();
            services.AddCors(opt => 
            {
              opt.AddPolicy("CorsPolicy",policy => 
              {
                  policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
              });
            });

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment()){}
            
                app.UseMiddleware<ExceptionMiddleware>();
                //app.UseDeveloperExceptionPage();
               
            
            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseStaticFiles();
            app.UseSwaggerDocumentation();

            app.UseAuthorization();
            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
