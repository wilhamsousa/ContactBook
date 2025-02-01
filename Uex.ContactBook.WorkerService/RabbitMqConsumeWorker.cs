using RabbitMQ.Client;
using Uex.ContactBook.Domain.Interfaces;
using Uex.ContactBook.Worker.RabbitMqConsumeStrategy;
using Uex.ContactBook.Worker.RabbitMqConsumeStrategy.Strategy;

namespace Uex.ContactBook.Worker
{
    public class RabbitMqConsumeWorker : BackgroundService
    {
        private readonly ILogger<RabbitMqConsumeWorker> _logger;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;

        private readonly IConnection _connection;
        private readonly IChannel _channel;

        public RabbitMqConsumeWorker(
            ILogger<RabbitMqConsumeWorker> logger,
            IConfiguration configuration,
            IServiceProvider serviceProvider)
        {
            _logger = logger;

            _configuration = configuration;
            _serviceProvider = serviceProvider;

            var factory = new ConnectionFactory()
            {
                HostName = _configuration.GetSection("Rabbitmq:HostName").Value,
                Port = Convert.ToInt16(_configuration.GetSection("Rabbitmq:Port").Value ?? "0"),
                UserName = _configuration.GetSection("Rabbitmq:Username").Value,
                Password = _configuration.GetSection("Rabbitmq:Password").Value
            };

            _connection = factory.CreateConnectionAsync().Result;
            _channel = _connection.CreateChannelAsync().Result;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            using (IServiceScope scope = _serviceProvider.CreateScope()) 
            {
                var userRepositoryAsync = scope.ServiceProvider.GetRequiredService<IUserRepositoryAsync>();

                var _rabbitMqResetEmail = new RabbitMqContext(new ResetEmailStategy(_channel, userRepositoryAsync));
                var consumer = _rabbitMqResetEmail.GetConsumer();
                string queueName = _rabbitMqResetEmail.GetQueueName();
                await _channel.BasicConsumeAsync(
                    queue: queueName,
                    autoAck: false,
                    consumer: consumer
                );
            
            }

            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    if (_logger.IsEnabled(LogLevel.Information))
            //    {
            //        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //    }
            //    await Task.Delay(1000, stoppingToken);
            //}
        }
    }
}
