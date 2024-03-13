using Microsoft.OpenApi.Models;
using MediatR;
using Weather.Application.Handlers;
using System.Reflection;
using Weather.Application.Behaviors;
using FluentValidation;
using System;
using Weather.Application.Commands;
using Weather.Application.Validators;
using FluentValidation.Validators;
using Hellang.Middleware.ProblemDetails;
using Weather.API.ProblemDetails;

namespace Weather.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // Configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Weather.API v1"));
            }

            app.UseProblemDetails();


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

        public void ConfigureServices(IServiceCollection services)
        {
            var assembly = typeof(Startup).Assembly;
            // services.AddMediatR(c => c.RegisterServicesFromAssembly(assembly));

            var executingAssembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining(typeof(CreateBookingCommandHandler)));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            //AddFluentValidation(services, executingAssembly);

            //services.AddScoped<IValidator<CreateBookingCommand>, CreateBookingCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateBookingCommandValidator>(ServiceLifetime.Scoped);


            //services.AddProblemDetails();
            services.AddProblemDetails(options =>
            {
                options.IncludeExceptionDetails = (ctx, ex) => { return false; };

                options.Map<ValidationException>(c => new BadRequestProblemDetails(c));
            });

            services.AddControllers();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Weather.API", Version = "v1" });
            });
        }

        private void AddFluentValidation( IServiceCollection services, Assembly assembly)
        {
            var validatorType = typeof(AbstractValidator<>);

            var types = assembly.GetExportedTypes();


            var validatorTypes = assembly
                .GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType &&
                    i.GetGenericTypeDefinition() == validatorType))
                .ToList();

            foreach (var validator in validatorTypes)
            {
                var requestType = validator.GetInterfaces()
                    .Where(i => i.IsGenericType &&
                        i.GetGenericTypeDefinition() == typeof(AbstractValidator<>))
                    .Select(i => i.GetGenericArguments()[0])
                    .First();

                var validatorInterface = validatorType
                    .MakeGenericType(requestType);

                services.AddTransient(validatorInterface, validator);
            }
        }
    }
}
