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
    
    public async Task<List<Chamado>> GetAll(Usuario usuario)
    {
        var query = _dbContext.Chamados
            .Include(c => c.Ativo)
            .ThenInclude(a => a.localizacao) 
            .Include(c => c.Ativo)
            .ThenInclude(a => a.Usuario) 
            .Where(a => a.Ativo.id_usuario == usuario.Id_usuario)
            .AsQueryable();

        return await query.ToListAsync();
    }

    public async Task<Chamado?> GetById(Usuario usuario, long id)
    {
        return await _dbContext.Chamados.Include(c => c.Ativo)
            .Where(a => a.Ativo.id_usuario == usuario.Id_usuario)
            .AsNoTracking().FirstOrDefaultAsync(chamados => chamados.Id_Chamado == id);
    }
    
    public async Task<List<Chamado>> FilterByMonth(Usuario usuario, DateOnly date)
    {
        var startDate = new DateTime(year: date.Year, month: date.Month, day: 1).Date;
        
        var daysInMonth = DateTime.DaysInMonth(year: date.Year, month: date.Month);
        var endDate = new DateTime(year: date.Year, month: date.Month, day: daysInMonth, hour: 23, minute: 59, second: 59);
        
        return await _dbContext
            .Chamados
            .Include(c => c.Ativo)
            .AsNoTracking()
            .Where(chamados => chamados.Ativo.id_usuario == usuario.Id_usuario && chamados.Data_Abertura >= startDate && chamados.Data_Abertura <= endDate)
            .OrderBy(chamados => chamados.Data_Abertura)
            .ThenBy(chamados => chamados.Titulo)
            .ToListAsync();
    }
    

    public void Update(Chamado chamado)
    {
        _dbContext.Chamados.Update(chamado);
    }
}