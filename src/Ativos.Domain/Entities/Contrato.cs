using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ativos.Domain.Entities;

public class Contrato
{
    [Key]
    [Column("id_contrato")]
    public long Id_Contrato { get; set; }
    public long Id_Ativo { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public string Descricao {get; set; } = string.Empty;
    public Double Valor { get; set; }
}