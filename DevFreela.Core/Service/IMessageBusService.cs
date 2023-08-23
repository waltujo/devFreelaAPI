namespace DevFreela.Core.Service
{
    public interface IMessageBusService
    {
        void Publish(string queue, byte[] message);
    }
}
