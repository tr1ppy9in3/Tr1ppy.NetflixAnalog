using MediatR;

using Tr1ppy.Cryptography.Password;
using Tr1ppy.NetflixAnalog.Security.Authentication.UseCases.Abstractions;

namespace Tr1ppy.NetflixAnalog.Security.Authentication.UseCases.Commands.Login;

public sealed class LoginCommandHandler
(
    ITokenService tokenService,
    IUserRepository userRepository,
    PasswordCryptographyService passwordCryptographyService
)
    : IRequestHandler<LoginCommand, string>
{
    private readonly ITokenService _tokenService = tokenService
        ?? throw new ArgumentNullException(nameof(tokenService));

    private readonly IUserRepository _userRepository = userRepository
        ?? throw new ArgumentNullException(nameof(userRepository)); 

    private readonly PasswordCryptographyService _passwordCryptographyService = passwordCryptographyService
        ?? throw new ArgumentNullException(nameof(passwordCryptographyService));
        
    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByLoginAsync(login: request.Login);
        if (existingUser is null)
        {
            return string.Empty;
        }

        if (existingUser.IsBlocked)
        {
            return string.Empty;
        }

        if (!_passwordCryptographyService.VerifyPassword(request.Password, existingUser.PasswordHash))
        {
            return string.Empty;
        }

        return _tokenService.GenerateAccessToken(existingUser);
    }
}
