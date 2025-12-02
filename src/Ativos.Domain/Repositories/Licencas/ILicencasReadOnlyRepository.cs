namespace Ativos.Domain.Repositories.Licencas;

public interface ILicencasReadOnlyRepository
{
    Task<List<Entities.Licenca>> GetAll();
    
    Task<Entities.Licenca> GetById(long id);
}