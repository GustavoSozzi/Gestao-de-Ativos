using Ativos.Domain.Entities;

namespace Ativos.Domain.Services.LoggedUser;

public interface ILoggedUser
{
    Task<Usuario> Get();
}