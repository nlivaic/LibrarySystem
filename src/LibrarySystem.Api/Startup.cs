using FluentValidation.AspNetCore;
using LibrarySystem.Api.Filters;
using LibrarySystem.Api.Helpers;
using LibrarySystem.Api.Middlewares;
using LibrarySystem.Application;
using LibrarySystem.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace LibrarySystem.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _hostEnvironment;

        public Startup(IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            _configuration = configuration;
            _hostEnvironment = hostEnvironment;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();
            services
                .AddControllers(configure =>
                {
                    configure.ReturnHttpNotAcceptable = true;
                    configure.Filters.Add(new ProducesResponseTypeAttribute(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest));
                    configure.Filters.Add(new ProducesResponseTypeAttribute(typeof(ProblemDetails), StatusCodes.Status404NotFound));
                    configure.Filters.Add(new ProducesResponseTypeAttribute(typeof(object), StatusCodes.Status406NotAcceptable));
                    configure.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
                    configure.Filters.Add(typeof(LoggingFilter));
                })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = actionContext =>
                    {
                        var actionExecutingContext = actionContext as ActionExecutingContext;
                        var validationProblemDetails = ValidationProblemDetailsFactory.Create(actionContext);
                        if (actionContext.ModelState.ErrorCount > 0
                            && actionExecutingContext?.ActionArguments.Count == actionContext.ActionDescriptor.Parameters.Count)
                        {
                            validationProblemDetails.Status = StatusCodes.Status422UnprocessableEntity;
                            return new UnprocessableEntityObjectResult(validationProblemDetails);
                        }
                        validationProblemDetails.Status = StatusCodes.Status400BadRequest;
                        return new BadRequestObjectResult(validationProblemDetails);
                    };
                })
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddDbContext<LibrarySystemDbContext>(options =>
            {
                options.UseNpgsql(_configuration.GetConnectionString("LibrarySystemDbConnection"));
                if (_hostEnvironment.IsDevelopment())
                    options.EnableSensitiveDataLogging(true);
            });
            services.AddControllers();
            services.AddLibrarySystemApplicationHandlers();
            services.AddGenericRepositories();
            services.AddSingleton<IScopeInformation, ScopeInformation>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "LibrarySystemOpenAPISpecification",
                    new OpenApiInfo
                    {
                        Title = "Library System API",
                        Version = "v1",
                        Description = "This API allows access to the Library System.",
                        Contact = new OpenApiContact
                        {
                            Name = "Nenad Livaic",
                            Url = new Uri("https://github.com/nlivaic")
                        },
                        License = new OpenApiLicense
                        {
                            Name = "MIT",
                            Url = new Uri("https://www.opensource.org/licenses/MIT")
                        },
                        TermsOfService = new Uri("https://www.my-terms-of-service.com")
                    });
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "LibrarySystem.Api.xml"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseApiExceptionHandler(options =>
            {
                options.ApiErrorHandler = UpdateApiErrorResponse;
                options.LogLevelHandler = LogLevelHandler;
            });
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/LibrarySystemOpenAPISpecification/swagger.json", "LibrarySystem.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapControllers();
            });
        }

        /// <summary>
        /// A demonstration of how returned message can be modified.
        /// </summary>
        private void UpdateApiErrorResponse(HttpContext context, Exception ex, ProblemDetails problemDetails)
        {
            //if (ex is SomeCustomException)
            //{
            //    problemDetails.Detail = "A general error occurred.";
            //}
        }

        /// <summary>
        /// Define cases where a different log level is needed for logging exceptions.
        /// </summary>
        private LogLevel LogLevelHandler(HttpContext context, Exception ex)
        {
            //if (ex is Exception)
            //{
            //    return LogLevel.Critical;
            //}
            //return LogLevel.Error;
            return LogLevel.Critical;
        }
    }
}
