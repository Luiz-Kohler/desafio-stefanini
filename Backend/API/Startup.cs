using Application;
using Application.Common.Exceptions;
using Domain;
using Infra;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Reflection;

namespace API
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddDomain();
            services.AddInfra();

            services.AddControllers();

            services.AddExceptionHandler(options =>
            {
                options.ExceptionHandler = GlobalExceptionHandler.Handle;
                options.AllowStatusCode404Response = true;
            });

            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                option.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCors(option => option
                .SetIsOriginAllowed(_ => true)
                .AllowAnyHeader()
                .WithMethods("POST", "PUT", "DELETE", "GET")
                .AllowCredentials()
            );

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseStaticFiles();
            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            ConfigureCulture();
        }

        private static void ConfigureCulture()
        {
            var ptBrCulture = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = ptBrCulture;
            CultureInfo.DefaultThreadCurrentUICulture = ptBrCulture;
        }
    }
}
