using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ativos.Domain.Entities;

public class Usuario
{
    [Key]
    [Column("id_usuario")]
    public long Id_usuario { get; set; }
    public string P_nome { get; set; } = string.Empty;
    public string Sobrenome {get; set; } = string.Empty;
    public long Matricula { get; set; }
    public string Departamento { get; set; } = string.Empty;
    public string Cargo { get; set; } = string.Empty;
    public List<Licenca> licencas { get; } = [];
}