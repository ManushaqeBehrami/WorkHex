using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace HexaFullStack.Infrastructure.Realtime;

[Authorize]
public sealed class NotificationsHub : Hub { }
