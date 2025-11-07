using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ativos.Domain.Entities;

public class Licenca
{
    [Key]
    [Column("id_licenca")]
    public long Id_Licenca { get; set; }
    public string Nome_Soft { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public DateTime Data { get; set; }
    public List<Usuario> Usuarios { get; } = [];
}