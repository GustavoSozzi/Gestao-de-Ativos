using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ativos.Domain.Enums;

namespace Ativos.Domain.Entities;

public class Licenca
{
    [Key]
    [Column("id_licenca")]
    public long Id_Licenca { get; set; }
    public TipoLicenca Tipo_Licenca { get; set; }
    public DateTime Data { get; set; }
    public List<Usuario> Usuarios { get; } = [];
}