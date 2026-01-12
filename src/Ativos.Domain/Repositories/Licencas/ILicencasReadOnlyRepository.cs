using Ativos.Domain.Entities;

namespace Ativos.Domain.Repositories.Licencas;

public interface ILicencasReadOnlyRepository
{
    Task<List<Entities.Licenca>> GetAll(Usuario usuario);
    
    Task<Entities.Licenca> GetById(long id);
}