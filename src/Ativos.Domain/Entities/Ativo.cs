using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ativos.Domain.Entities;

public class Ativo
{
    [Key]
    [Column("id_ativo")]
    public long Id_ativo { get; set; }
    
    [Column("id_localizacao")]
    public long id_localizacao { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Modelo {get; set; } = string.Empty;
    public string SerialNumber { get; set; } = string.Empty;
    public long CodInventario { get; set; }
    public string? Tipo { get; set; }
    
    [ForeignKey("id_localizacao")]
    public Localizacao localizacao { get; set; } = null!;
    public ICollection<Chamado> Chamados { get;} = new List<Chamado>();
}