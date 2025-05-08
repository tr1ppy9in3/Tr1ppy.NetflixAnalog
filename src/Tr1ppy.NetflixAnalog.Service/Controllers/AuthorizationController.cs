using MediatR;


using Microsoft.AspNetCore.Mvc;

using Tr1ppy.NetflixAnalog.Security.Authentication.Contracts;
using Tr1ppy.NetflixAnalog.Security.Authentication.UseCases.Commands.Register;

namespace Tr1ppy.NetflixAnalog.Service.Controllers;

[ApiController]
[Route("api/authorization/registration")]
public class AuthorizationController
(
    IMediator mediator,
    IUserAccessor userAccessor
)
{
    private readonly IMediator _mediator = mediator
        ?? throw new ArgumentNullException(nameof(mediator));

    private readonly IUserAccessor _userAccessor = userAccessor
        ?? throw new ArgumentNullException(nameof(userAccessor));

    [HttpPost]
    public async Task<IActionResult> Registration(RegisterCommand registerCommand)
    {
        var result = await _mediator.Send(registerCommand);
        return new OkObjectResult(new object());
    }
}
