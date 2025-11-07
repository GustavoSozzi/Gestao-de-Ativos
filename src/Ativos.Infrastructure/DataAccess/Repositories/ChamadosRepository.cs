using Ativos.Domain.Entities;
using Ativos.Domain.Repositories.Chamados;
using Microsoft.EntityFrameworkCore;

namespace Ativos.Infrastructure.DataAccess.Repositories;

internal class ChamadosRepository : IChamadosWriteOnlyRepository
{
    private readonly AtivosDbContext _dbContext;

    public ChamadosRepository(AtivosDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task Add(Chamado chamado)
    {
        await _dbContext.Chamados.AddAsync(chamado);
    }
}