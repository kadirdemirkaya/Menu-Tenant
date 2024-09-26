namespace Shared.Stream
{
    public interface IStreamEventHandler<TEvent>
        where TEvent : IStreamEvent
    {
        Task StreamHandler(TEvent @event);
    }
}
