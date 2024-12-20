using Uex.ContactBook.Api.Controllers.Notification;
using Uex.ContactBook.Api.Extensions;
using Uex.ContactBook.Infra.Repositories.Context;
using Hellang.Middleware.ProblemDetails;
using Microsoft.EntityFrameworkCore;

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
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContextPool<ContactBookContext>(
            options => options
                .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
        );

        using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
        ILogger logger = factory.CreateLogger("Program");
        logger.LogInformation("Application started!");
        logger.LogInformation("DefaultConnection: " + builder.Configuration.GetConnectionString("DefaultConnection"));

        builder.Services.AddMvc(options => options.Filters.Add<NotificationFilter>());
        //            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);        

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