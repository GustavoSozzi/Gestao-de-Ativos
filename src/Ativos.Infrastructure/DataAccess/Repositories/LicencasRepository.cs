using Ativos.Domain.Entities;
using Ativos.Domain.Repositories.Licencas;

namespace Ativos.Infrastructure.DataAccess.Repositories;

internal class LicencasRepository : ILicencasWriteOnlyRepository
{
    private readonly AtivosDbContext _dbContext;

    public LicencasRepository(AtivosDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task Add(Licenca licenca)
    {
        await _dbContext.Licencas.AddAsync(licenca);
    }
}