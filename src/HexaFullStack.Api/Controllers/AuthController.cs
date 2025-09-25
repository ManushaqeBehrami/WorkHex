using Microsoft.AspNetCore.Mvc;
using HexaFullStack.Application.UseCases.Users;

namespace HexaFullStack.Api.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController : ControllerBase
{
    [HttpPost("register")]
    public async Task<IResult> Register([FromServices] RegisterUser uc, [FromBody] RegisterUser.Command cmd, CancellationToken ct)
        => Results.Ok(await uc.HandleAsync(cmd, ct));

    [HttpPost("login")]
    public async Task<IResult> Login([FromServices] LoginUser uc, [FromBody] LoginUser.Command cmd, CancellationToken ct)
        => Results.Ok(await uc.HandleAsync(cmd, ct));
}
