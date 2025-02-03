using Uex.ContactBook.Application.Base;
using Uex.ContactBook.Domain.Interfaces;
using Uex.ContactBook.Domain.Notification;
using Microsoft.Extensions.Configuration;
using Uex.ContactBook.Domain.Interfaces.External;
using Uex.ContactBook.Domain.Model.DTOs.External;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Uex.ContactBook.Application.Services
{
    public class ExternalServiceAsync : ServiceBase, IExternalServiceAsync
    {
        private readonly IConfiguration _configuration;

        public ExternalServiceAsync(
            NotificationContext notificationContext,
            IConfiguration configuration
        ) : base(notificationContext)
        {
            _configuration = configuration;
        }

        public async Task<ViaCepResponse> GetViaCep(string cep)
        {
            string url = $"https://viacep.com.br/ws/{cep}/json/";
            var client = new HttpClient();
            var responseStr = await client.GetStringAsync(url);

            if (string.IsNullOrEmpty(responseStr))
                return new ViaCepResponse();

            var response = JsonConvert.DeserializeObject<ViaCepResponse>(responseStr);
            return response;
        }

        public async Task RabbitMqProduce(string queueName, string message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _configuration.GetSection("Rabbitmq:HostName").Value,
                Port = Convert.ToInt16(_configuration.GetSection("Rabbitmq:Port").Value ?? "0"),
                UserName = _configuration.GetSection("Rabbitmq:Username").Value,
                Password = _configuration.GetSection("Rabbitmq:Password").Value
            };

            var connection = await factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            var body = Encoding.UTF8.GetBytes(message);

            await SendMessageByQueue(queueName, channel, body);
            //await SendMessageByExchange(queueName, channel, body);

            Console.WriteLine($"Message Published {message}");
        }

        private static async Task SendMessageByQueue(string queueName, IChannel channel, byte[] body)
        {
            await channel.QueueDeclareAsync(
                            queue: queueName,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null
                        );

            await channel.BasicPublishAsync(
                exchange: string.Empty,
                routingKey: queueName,
                body: body
            );
        }

        private static async Task SendMessageByExchange(string exchangeName, IChannel channel, byte[] body)
        {
            await channel.ExchangeDeclareAsync(
                            exchange: exchangeName,
                            type: ExchangeType.Fanout
                        );

            await channel.BasicPublishAsync(
                exchange: exchangeName,
                routingKey: string.Empty,
                body: body
            );
        }
    }
}
