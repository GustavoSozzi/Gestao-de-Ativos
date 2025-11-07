using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ativos.Domain.Entities;

public class Localizacao
{
    [Key]
    [Column("id_localizacao")]
    public long Id_Localizacao { get; set; }
    public string Cidade {get; set; } = string.Empty;
    public string Estado {get; set; } = string.Empty;
    
    public ICollection<Ativo> ativos { get;} = new List<Ativo>();
}