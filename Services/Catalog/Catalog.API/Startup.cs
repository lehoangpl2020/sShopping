using Catalog.Application.Queries;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data;
using Catalog.Infrastructure.Data.Repositories;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Catalog.API;

public class Startup
{
    public IConfiguration Configuration;

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }


    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApiVersioning();



        services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog.API", Version = "v1" }); });

        //Redis Settings
        //services.AddStack (options =>
        //{
        //    options.Configuration = Configuration.GetValue<string>("CacheSettings:ConnectionString");
        //});


        // DI
        services.AddAutoMapper(typeof(Startup));

        var assembly = Assembly.GetExecutingAssembly();
        // services.AddMediatR(c => c.RegisterServicesFromAssembly(assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(GetAllProductQuery)));


        services.AddScoped<ICatalogContext, CatalogContext>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IBrandRepository, ProductRepository>();
        services.AddScoped<ITypesRepository, ProductRepository>();

        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog API v1"));
        }

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();


        });
    }
}