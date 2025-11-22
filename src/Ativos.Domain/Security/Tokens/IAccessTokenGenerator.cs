using Ativos.Domain.Entities;

namespace Ativos.Domain.Security.Tokens;

public interface IAccessTokenGenerator
{
    string Generate(Usuario usuario);
}