using MediatR;

namespace Tr1ppy.NetflixAnalog.Security.Authentication.UseCases.Commands.Register;

public sealed class RegisterCommand : IRequest<Unit>
{
    public required string Login { get; set; }

    public required string Password { get; set; }
}
