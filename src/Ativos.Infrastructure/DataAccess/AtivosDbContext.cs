using Microsoft.EntityFrameworkCore;
using Ativos.Domain.Entities;


internal class AtivosDbContext : DbContext
{
    public AtivosDbContext(DbContextOptions options) : base(options) {}
    
    //Segundo parametro: nome da tabela no banco de dados
    public DbSet<Ativo> Ativos { get; set; }
    public DbSet<Usuario> Usuario { get; set; }
    public DbSet<Chamado> Chamados {get; set;}
    public DbSet<Contrato> Contratos { get; set; }
    public DbSet<Licenca> Licencas { get; set; }
    public DbSet<Localizacao> Localizacao { get; set; }
}