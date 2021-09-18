using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreditCardService.Data.Interfaces;
using CreditCardService.Repositories;
using CreditCardService.Repositories.Interfaces;
using CreditCardService.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CreditCardService.Data;

namespace CreditCardService
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
            services.Configure<CreditCardDatabaseSettings>(Configuration.GetSection(nameof(CreditCardDatabaseSettings)));
            services.AddSingleton<ICreditCardDatabaseSettings>(sp =>
             sp.GetRequiredService<IOptions<CreditCardDatabaseSettings>>().Value);

            services.AddTransient<ICreditCardContext, CreditCardContext>();
            services.AddTransient<ICreditCardRepository, CreditCardRepository>();

            services.AddSwaggerGen(c => 
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo{ Title = "CreditCardService API", Version = "v1" });
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
              c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog API V1")
            );
        }
    }
}
