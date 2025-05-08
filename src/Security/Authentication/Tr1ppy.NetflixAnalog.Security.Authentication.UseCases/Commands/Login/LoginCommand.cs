using MediatR;

namespace Tr1ppy.NetflixAnalog.Security.Authentication.UseCases.Commands.Login;

public sealed class LoginCommand : IRequest<string>
{
    public required string Login { get; set; }

    public required string Password { get; set; }
}