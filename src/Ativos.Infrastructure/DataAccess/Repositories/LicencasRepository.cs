using Ativos.Domain.Entities;
using Ativos.Domain.Repositories.Licencas;
using Microsoft.EntityFrameworkCore;

namespace Ativos.Infrastructure.DataAccess.Repositories;

internal class LicencasRepository : ILicencasWriteOnlyRepository, ILicencasReadOnlyRepository
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
    
    public async Task AddWithUsuario(Licenca licenca, long usuarioId)
    {
        // Buscar usuário com tracking habilitado
        var usuario = await _dbContext.Usuario
            .FirstOrDefaultAsync(u => u.Id_usuario == usuarioId);
        
        if (usuario != null)
        {
            licenca.Usuarios.Add(usuario);
        }
        
        await _dbContext.Licencas.AddAsync(licenca);
    }

    public async Task<List<Licenca>> GetAll()
    {
        return await _dbContext.Licencas.ToListAsync();
    }

    public async Task<Licenca?> GetById(long id)
    {
        return await _dbContext.Licencas.AsNoTracking().FirstOrDefaultAsync(licencas => licencas.Id_Licenca == id);
    }
}