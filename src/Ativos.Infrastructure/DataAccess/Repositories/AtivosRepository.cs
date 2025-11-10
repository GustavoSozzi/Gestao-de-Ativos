using Ativos.Domain.Entities;
using Ativos.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ativos.Infrastructure.DataAccess.Repositories;

internal class AtivosRepository : IAtivosReadOnlyRepository, IAtivosWriteOnlyRepository, IAtivosUpdateOnlyRepository
{
    //encapsulam o acesso a dados. Eles escondem os detalhes do banco de dados e expõem métodos para adicionar, buscar, atualizar ou remover entidades
    private readonly AtivosDbContext _dbContext;

    public AtivosRepository(AtivosDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Ativo ativo)
    {
        await _dbContext.Ativos.AddAsync(ativo);
    }

    public async Task<List<Ativo>> GetAll()
    {
        return await _dbContext.Ativos
            .Include(a => a.localizacao)
            .ToListAsync();
    }

    public async Task<Ativo?> GetById(long id)
    {
        return await _dbContext.Ativos.AsNoTracking().FirstOrDefaultAsync(ativo => ativo.Id_ativo == id);
    }

    public void Update(Ativo ativo)
    {
        _dbContext.Ativos.Update(ativo);
    }
    
    public async Task<bool> Delete(long id)
    {
        var result = await _dbContext.Ativos.FirstOrDefaultAsync(ativo => ativo.Id_ativo == id);

        if(result is null)
        {
            return false;
        }

        _dbContext.Ativos.Remove(result);

        return true;
    }
}
