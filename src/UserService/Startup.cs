using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Internal;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using UserService.Data;
using UserService.Data.Interfaces;
using UserService.Mapper;
using UserService.Repositories;
using UserService.Repositories.Interfaces;

namespace UserService
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

            // MassTransit-RabbitMQ Configuration
            services
                .AddMassTransit(config =>
                {
                    config
                        .UsingRabbitMq((ctx, cfg) =>
                        {
                            cfg
                                .Host(Configuration["EventBusSettings:HostAddress"]);
                        });
                });
            services.AddMassTransitHostedService();

            services.AddTransient<IUserContext, UserContext>();
            services.AddTransient<IUserRepository, UserRepository>();
          
            var config =
                new AutoMapper.MapperConfiguration(cfg =>
                    {
                        cfg.AddProfile(new UserProfile());
                    });

            var mapper = config.CreateMapper();
            services.AddSingleton (mapper);
            services
                .AddSwaggerGen(c =>
                {
                    c
                        .SwaggerDoc("v1",
                        new Microsoft.OpenApi.Models.OpenApiInfo {
                            Title = "UserService API",
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
                        "UserService API V1"));
        }
    }
}
