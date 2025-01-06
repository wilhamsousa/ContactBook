namespace Uex.ContactBook.Api.Extensions.Services
{
    public static partial class CorsConfiguration
    {
        public static void AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", builder =>
                    builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                );

            });
        }
    }
}
