using Ativos.Domain.Entities;
using Ativos.Domain.Repositories.Chamados;
using Microsoft.EntityFrameworkCore;

namespace Ativos.Infrastructure.DataAccess.Repositories;

internal class ChamadosRepository : IChamadosWriteOnlyRepository, IChamadosReadOnlyRepository, IChamadosUpdateOnlyRepository
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
    
    public async Task<List<Chamado>> GetAll()
    {
        var query = _dbContext.Chamados
            .Include(a => a.Ativo)
            .AsQueryable();

        return await query.ToListAsync();
    }

    public async Task<Chamado?> GetById(long id)
    {
        return await _dbContext.Chamados.AsNoTracking().FirstOrDefaultAsync(chamados => chamados.Id_Chamado == id);
    }

    public void Update(Chamado chamado)
    {
        throw new NotImplementedException();
    }
}