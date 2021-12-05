using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WebShop.Infrastructure;
using WebShop.Mapping;
using WebShop.Repository.Contract.Interfaces;
using WebShop.Repository.Repositories;
using WebShop.Service.Contract.Services;
using WebShop.Service.Services;

namespace WebShop
{
    public class Startup
    {
        private readonly string _cors = "cors";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddControllers().AddJsonOptions(options =>
                                                 options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            services.AddDbContext<WebShopDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("WebShopDatabase")));


            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web Shop API", Version = "v1" });

            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: _cors, builder => {
                    builder.WithOrigins("https://localhost:3000", "http://localhost:3000").AllowAnyHeader()
                                        .AllowAnyMethod().AllowCredentials();
                });
            });



            //Add Service implementations
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
          

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebShop v1"));
            }

            app.UseHttpsRedirection();
            app.UseCors(_cors);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<WebShopDbContext>();
                context.Database.Migrate();
            }
        }
    }
}
