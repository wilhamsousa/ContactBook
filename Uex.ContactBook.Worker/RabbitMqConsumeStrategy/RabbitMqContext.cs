using RabbitMQ.Client.Events;
using Uex.ContactBook.Worker.RabbitMqConsumeStrategy.Interface;

namespace Uex.ContactBook.Worker.RabbitMqConsumeStrategy
{
    public class RabbitMqContext
    {
        private readonly IConsumeStrategy _consume;

        public RabbitMqContext(IConsumeStrategy consume)
        {
            _consume = consume;
        }

        public AsyncEventingBasicConsumer GetConsumer() =>
            _consume.GetConsume();
    }
}
