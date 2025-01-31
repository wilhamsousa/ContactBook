using Uex.ContactBook.Worker;
using Uex.ContactBook.Worker.Extensions.Services;

var builder = Host.CreateApplicationBuilder(args);
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddContextConfiguration(connectionString);
builder.Services.AddDependencyInjections();
builder.Services.AddHostedService<RabbitMqConsumeWorker>();

var host = builder.Build();
host.Run();
