using MediatR;

using Tr1ppy.Cryptography.Password;
using Tr1ppy.NetflixAnalog.Security.Authentication.Core;
using Tr1ppy.NetflixAnalog.Security.Authentication.UseCases.Abstractions;

namespace Tr1ppy.NetflixAnalog.Security.Authentication.UseCases.Commands.Register;

public sealed class RegisterCommandHandler
(
    IUserRepository userRepository,
    PasswordCryptographyService passwordCryptographyService
) 
    : IRequestHandler<RegisterCommand, Unit>
{
    private readonly IUserRepository _userRepository = userRepository
        ?? throw new ArgumentNullException(nameof(userRepository));

    private readonly PasswordCryptographyService _passwordCryptographyService = passwordCryptographyService
       ?? throw new ArgumentNullException(nameof(passwordCryptographyService));

    public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.ExistsByLogin(request.Login))
        {
            return Unit.Value;
        }

        var user = new User()
        {
            Login = request.Login,
            PasswordHash = _passwordCryptographyService.HashPassword(request.Password),
            Roles = [UserRole.Base]
        };

        await _userRepository.Create(user);
        return Unit.Value;
    }
}
