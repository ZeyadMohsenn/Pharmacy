using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Application.Services.TokenService;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Identity;

namespace Pharmacy.Application.Features.Auth.Command.Login;

public class LoginCommandHandler(UserManager<ApplicationUser> userManager, ITokenService tokenService)
 :  BaseHandler<LoginCommand, Result<TokenDto>>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly ITokenService _tokenService = tokenService;

    public override async Task<Result<TokenDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName);

        if (user == null || user.Is_Deleted)
            return Result<TokenDto>.Fail(Messages.UserNotFound);

        if (!user.Is_Active)
            return Result<TokenDto>.Fail(Messages.YourAccountIsDeactivated);

        var result = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!result)
            return Result<TokenDto>.Fail(Messages.IncorrectPassword);

        var token = await _tokenService.GenerateToken(user);
        return Result<TokenDto>.Success(token);
    }
}
