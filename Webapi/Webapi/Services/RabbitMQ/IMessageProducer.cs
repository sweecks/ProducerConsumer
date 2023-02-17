namespace Webapi.Services.RabbitMQ
{
    public interface IMessageProducer
    {
        void SendTextMessage(string queueName, string message);
    }
}
