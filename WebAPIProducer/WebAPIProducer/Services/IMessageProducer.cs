namespace WebAPIProducer.Services
{
    public interface IMessageProducer
    {
        public void SendMessageAsync<T>(T message);
    }
}
