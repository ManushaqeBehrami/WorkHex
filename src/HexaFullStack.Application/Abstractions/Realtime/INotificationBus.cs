namespace HexaFullStack.Application.Abstractions.Realtime;
public interface INotificationBus
{
    Task BroadcastAsync(string topic, object payload, CancellationToken ct);
    Task SendToUserAsync(Guid userId, string topic, object payload, CancellationToken ct);
}
