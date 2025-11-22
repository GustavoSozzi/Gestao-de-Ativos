using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ativos.Domain.Entities;
using Ativos.Domain.Security.Tokens;
using Ativos.Infrastructure.Migrations;
using Microsoft.IdentityModel.Tokens;

namespace Ativos.Infrastructure.Security.Tokens;

internal class JwtTokenGenerator : IAccessTokenGenerator
{
    private readonly uint _expirationTimeMinutes; //tempo de expiracao
    private readonly string _signingKey; //chave de assinatura

    public JwtTokenGenerator(uint expirationTimeMinutes, string signingKey)
    {
        _expirationTimeMinutes = expirationTimeMinutes;
        _signingKey = signingKey;
    }

    public string Generate(Usuario usuario)
    {
        var claims = new List<Claim>() //propiedades da claim
        {
            new Claim(ClaimTypes.Name, usuario.P_nome),
            new Claim(ClaimTypes.Sid, usuario.UserIdentifier.ToString())
        };
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddMinutes(_expirationTimeMinutes),
            SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256Signature),
            Subject = new ClaimsIdentity(claims) //propiedades desejadas do token, ex: nome do usuario e etc...
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken);
    }

    private SymmetricSecurityKey SecurityKey()
    {
        var key = Encoding.UTF8.GetBytes(_signingKey);
        
        return new SymmetricSecurityKey(key);
    }
}