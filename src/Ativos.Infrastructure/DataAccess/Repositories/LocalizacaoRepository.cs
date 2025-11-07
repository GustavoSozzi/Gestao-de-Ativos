using Ativos.Domain.Entities;
using Ativos.Domain.Repositories.Localizacao;
using Microsoft.EntityFrameworkCore;

namespace Ativos.Infrastructure.DataAccess.Repositories;

internal class LocalizacaoRepository : ILocalizacaoWriteOnlyRepository, ILocalizacaoReadOnlyRepository
{
    private readonly AtivosDbContext _dbContext;

    public LocalizacaoRepository(AtivosDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Localizacao>> GetAll()
    {
        return await _dbContext.Localizacao.ToListAsync();
    }
    
    public async Task Add(Localizacao localizacao)
    {
        await _dbContext.Localizacao.AddAsync(localizacao);
    }
}