using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Ativos.Domain.Entities;
using Ativos.Domain.Security.Tokens;
using Ativos.Domain.Services.LoggedUser;
using Ativos.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ativos.Infrastructure.Services.LoggedUser;

internal class LoggedUser : ILoggedUser
{
    private readonly AtivosDbContext _dbContext;
    private readonly ITokenProvider _tokenProvider;

    public LoggedUser(AtivosDbContext dbContext, ITokenProvider tokenProvider)
    {   
        _dbContext = dbContext;
        _tokenProvider = tokenProvider;
    }
    
    public async Task<Usuario> Get()
    {
        string token = _tokenProvider.TokenOnRequest();

        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

        var identifier = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;
        
        return await _dbContext.Usuario.AsNoTracking().FirstAsync(usuario => usuario.UserIdentifier == Guid.Parse(identifier));
    }
}