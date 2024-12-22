namespace Uex.ContactBook.Api.Extensions.Services
{
    public static partial class CorsConfiguration
    {
        public static void AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                );

                //builder.WithOrigins("https://localhost:44356").AllowAnyMethod().AllowAnyHeader();
                //builder.WithOrigins("https://localhost:5001").AllowAnyMethod().AllowAnyHeader();
            });
        }
    }
}
