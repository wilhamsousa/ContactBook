using RabbitMQ.Client.Events;

namespace Uex.ContactBook.Worker.RabbitMqConsumeStrategy.Interface
{
    public interface IConsumeStrategy
    {
        AsyncEventingBasicConsumer GetConsume();
        string GetQueueName();
    }
}
