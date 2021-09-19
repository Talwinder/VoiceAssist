using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountService.Data;
using AccountService.Data.Interfaces;
using AccountService.Exceptions;
using AccountService.Repositories;
using AccountService.Repositories.Interfaces;
using AccountService.Repositories.Interfaces.StaticClassesInterfaces;
using AccountService.Repositories.Wrappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace AccountService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services
                .AddSingleton<ConnectionMultiplexer>(sp =>
                {
                    var configuration =
                        ConfigurationOptions
                            .Parse(Configuration.GetConnectionString("Redis"),
                            true);
                    return ConnectionMultiplexer.Connect(configuration);
                });

            services.AddTransient<IAccountContext, AccountContext>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IFundRepository, FundRepository>();
            services.AddTransient<ISendReqBankAPI, SendReqBankAPIWrapper>();
            services.AddTransient<IJsonUtility, NewtonsoftJsonWrapper>();

            services
                .AddSwaggerGen(c =>
                {
                    c
                        .SwaggerDoc("v1",
                        new Microsoft.OpenApi.Models.OpenApiInfo {
                            Title = "Account API",
                            Version = "v1"
                        });
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });

            app.UseSwagger();
            app
                .UseSwaggerUI(c =>
                    c
                        .SwaggerEndpoint("/swagger/v1/swagger.json",
                        "Account API V1"));
        }
    }
}
