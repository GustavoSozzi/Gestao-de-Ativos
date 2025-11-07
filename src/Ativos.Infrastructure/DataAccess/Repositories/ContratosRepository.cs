using Ativos.Domain.Entities;
using Ativos.Domain.Repositories;
using Ativos.Domain.Repositories.Contratos;

namespace Ativos.Infrastructure.DataAccess.Repositories;

internal class ContratosRepository : IContratosWriteOnlyRepository
{
    private readonly AtivosDbContext _dbContext;

    public ContratosRepository(AtivosDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task Add(Contrato contrato)
    {
        await _dbContext.Contratos.AddAsync(contrato);
    }
}