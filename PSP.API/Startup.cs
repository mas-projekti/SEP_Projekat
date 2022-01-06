using AutoMapper;
using Common.CustomMiddleware;
using Common.Identity;
using Common.ServiceDiscovery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PSP.API.Infrastructure;
using PSP.API.Interfaces;
using PSP.API.Mapping;
using PSP.API.Services;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PSP.API
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
            ConfigureConsul(services);
            services.AddControllers().AddJsonOptions(options =>
                                     options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())); ;

            services.AddDbContext<PaymentServiceProviderDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("TransactionDatabase")));

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PSP.API", Version = "v1" });
            });


            services.AddCors(options =>
            {
                options.AddPolicy(name: _cors, builder => {
                    builder.AllowAnyHeader()
                           .AllowAnyMethod().AllowCredentials();
                });
            });

            #region Auth
            services.AddAuthentication("Bearer")
             .AddJwtBearer("Bearer", options =>
             {
                 options.Authority = Configuration.GetIdentityConfig().IdentityURL.ToString();
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateAudience = false
                 };
             });
            #endregion

            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IClientService, ClientService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PSP.API v1"));
            }
            app.UseCustomExceptionHandler();
            app.UseHttpsRedirection();
            app.UseCors(_cors);


            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<PaymentServiceProviderDbContext>();
                context.Database.Migrate();
            }
            app.UseSerilogRequestLogging();
        }

        private void ConfigureConsul(IServiceCollection services)
        {
            var serviceConfig = Configuration.GetServiceConfig();

            services.RegisterConsulServices(serviceConfig);
        }
    }
}
