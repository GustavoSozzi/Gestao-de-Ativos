using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ativos.Domain.Enums;

namespace Ativos.Domain.Entities;

public class Chamado
{
    [Key]
    [Column("id_chamado")]
    public long Id_Chamado { get; set; }
    public DateTime Data_Abertura { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Solucao { get; set; } =  string.Empty;
    public StatusChamado Status_Chamado { get; set; }
    
    [ForeignKey("Ativo")]
    public long id_Ativo { get; set; }
    public Ativo Ativo { get; set; } = null!; //chamado lado dependente pela relacao (0,n)
}