
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Uex.ContactBook.Api.Extensions.Services
{
    public static partial class AuthenticationConfiguration
    {
        public static void AddAuthenticationConfiguration(this IServiceCollection services, string issuer, string audience, string secret)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(secret);
            var secretBase64 = Convert.ToBase64String(bytes);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(options =>
                        {
                            options.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuer = true,
                                ValidateAudience = true,
                                ValidateLifetime = true,
                                ValidateIssuerSigningKey = true,
                                ValidIssuer = issuer,
                                ValidAudience = audience,
                                IssuerSigningKey = new SymmetricSecurityKey(
                                    Encoding.UTF8.GetBytes(secret))
                            };

                            options.Events = new JwtBearerEvents
                            {
                                OnAuthenticationFailed = c =>
                                {
                                    c.NoResult();
                                    c.Response.StatusCode = 401;
                                    c.Response.ContentType = "text/plain";

#if DEBUG
                                    c.Response.WriteAsync(c.Exception.ToString());
                                    return Task.CompletedTask;
#endif
                                    c.Response.WriteAsync("Erro na autenticação");
                                    return Task.CompletedTask;
                                },
                                OnTokenValidated = c =>
                                {
#if DEBUG
                                    System.Console.WriteLine("Token valido: " + c.SecurityToken);
#endif
                                    return Task.CompletedTask;
                                }
                            };
                        });
        }
    }
}