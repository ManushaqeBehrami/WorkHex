using Microsoft.AspNetCore.SignalR;
using HexaFullStack.Application.Abstractions.Realtime;

namespace HexaFullStack.Infrastructure.Realtime;
public sealed class SignalRNotificationBus : INotificationBus
{
    private readonly IHubContext<NotificationsHub> _hub;
    public SignalRNotificationBus(IHubContext<NotificationsHub> hub) => _hub = hub;

    public Task BroadcastAsync(string topic, object payload, CancellationToken ct)
        => _hub.Clients.All.SendAsync(topic, payload, ct);

    public Task SendToUserAsync(Guid userId, string topic, object payload, CancellationToken ct)
        => _hub.Clients.User(userId.ToString()).SendAsync(topic, payload, ct);
}
