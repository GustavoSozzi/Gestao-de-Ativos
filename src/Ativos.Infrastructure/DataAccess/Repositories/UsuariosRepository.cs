using Ativos.Domain.Entities;
using Ativos.Domain.Repositories.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace Ativos.Infrastructure.DataAccess.Repositories;

internal class UsuariosRepository : IUsuariosWriteOnlyRepository
{
    private readonly AtivosDbContext _dbContext;
    
    public UsuariosRepository(AtivosDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Add(Usuario usuario)
    {
        await _dbContext.Usuario.AddAsync(usuario);
    }
}