using WebAPI.Shared.Events;

namespace WebAPI.Application.Common.Events;

public interface IEventPublisher : ITransientService
{
    Task PublishAsync(IEvent @event);
}