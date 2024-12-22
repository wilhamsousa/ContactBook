
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Uex.ContactBook.Api.Extensions.Services
{
    public static partial class SwaggerConfiguration
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Contact Book API",
                        Description = "Contact Book API Endpoints",
                        Version = "v1",
                        TermsOfService = null,
                        Contact = new OpenApiContact
                        {
                        },
                        License = new OpenApiLicense
                        {
                        }
                    });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                x.IncludeXmlComments(xmlPath);

                xmlFile = "Uex.ContactBook.Domain.xml";
                xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                x.IncludeXmlComments(xmlPath);
            });
            services.AddSwaggerGenNewtonsoftSupport();
        }

    }
}
