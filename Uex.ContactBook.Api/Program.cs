using Uex.ContactBook.Api.Controllers.Notification;
using Uex.ContactBook.Api.Extensions;
using Hellang.Middleware.ProblemDetails;
using Microsoft.EntityFrameworkCore;
using Uex.ContactBook.Api.Extensions.Services;
using Swashbuckle.AspNetCore.SwaggerUI;

internal class Program
{
    private static void Main(string[] args)
    {
        MappingConfiguration.AddMapping();

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddProblemDetailsConfiguration();
        builder.Services.AddDependencyInjections();

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        
        builder.Services.AddSwagger();

        string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddContextConfiguration(connectionString);

        builder.Services.AddCorsConfiguration();

        builder.Services.AddMvc(options => options.Filters.Add<NotificationFilter>());

        var app = builder.Build();
        
        app.UseProblemDetails();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        if (!app.Environment.IsDevelopment() && !app.Environment.IsStaging())
            app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.ApplyMigration();

        app.Run();
    }
}