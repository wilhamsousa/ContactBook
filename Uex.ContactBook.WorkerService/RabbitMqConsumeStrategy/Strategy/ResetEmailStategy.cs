using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Uex.ContactBook.Domain.Interfaces;
using Uex.ContactBook.Infra.Repositories;
using Uex.ContactBook.Worker.RabbitMqConsumeStrategy.Interface;

namespace Uex.ContactBook.Worker.RabbitMqConsumeStrategy.Strategy
{
    public class ResetEmailStategy : IConsumeStrategy
    {
        private readonly IChannel _channel;
        private readonly IUserRepositoryAsync _userRepositoryAsync;

        public ResetEmailStategy(
            IChannel channel,
            IUserRepositoryAsync userRepositoryAsync)
        {
            _channel = channel;
            _userRepositoryAsync = userRepositoryAsync;
        }

        public AsyncEventingBasicConsumer GetConsume()
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    string message = Encoding.UTF8.GetString(body);
                    var user = _userRepositoryAsync.GetByEmailAsync(message);
#if DEBUG
                    Console.WriteLine("Rabbitmq - Received {0}", message);
#endif
                    return Task.CompletedTask;
                };

            return consumer;
        }

        public string GetQueueName() =>
            "ResetEmail";
    }
}
