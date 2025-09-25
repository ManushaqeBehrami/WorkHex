using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HexaFullStack.Application.Abstractions.Realtime;

namespace HexaFullStack.Api.Controllers;

[ApiController]
[Route("api/notifications")]
[Authorize(Roles = "Admin")]
public sealed class NotificationsController : ControllerBase
{
    private readonly INotificationBus _bus;
    public NotificationsController(INotificationBus bus) => _bus = bus;

    [HttpPost("broadcast")]
    public async Task<IResult> Broadcast([FromBody] object payload, CancellationToken ct)
    {
        await _bus.BroadcastAsync("app:announcement", payload, ct);
        return Results.Ok(new { ok = true });
    }
}
