using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Services.TokenService;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Auth.Command.RefreshToken;

public class RefreshTokenHandler(IUnitOfWork unitOfWork,ITokenService tokenService) : BaseHandler<RefreshTokenCommand, Result<TokenDto>>
{
    private readonly IGenericRepository<Domain.Entities.Auth.RefreshToken> _refreshTokenRepository = unitOfWork.GetRepository<Domain.Entities.Auth.RefreshToken>();
    private readonly ITokenService _tokenService = tokenService;

    public override async Task<Result<TokenDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = await _refreshTokenRepository.FindAsync(
            x => x.Token == request.RefreshToken, Include: x => x.Include(x => x.User));

        if (refreshToken == null || refreshToken.Is_Expired)
        {
            // Remove the expired refresh token
            if (refreshToken?.Is_Expired == true){
                _refreshTokenRepository.Remove(refreshToken);
                await unitOfWork.SaveChangesAsync();
            }
            return Result<TokenDto>.Fail();
        }

        var user = refreshToken.User;
        var token = await _tokenService.GenerateToken(user);

        // Remove the revoked refresh token
        _refreshTokenRepository.Remove(refreshToken);
        await unitOfWork.SaveChangesAsync();

        return Result<TokenDto>.Success(token);
    }
}
