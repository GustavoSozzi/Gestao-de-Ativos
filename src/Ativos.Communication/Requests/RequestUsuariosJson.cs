using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ativos.Communication.Requests;

public class RequestUsuariosJson
{
    public string P_nome { get; set; } = string.Empty;
    public string Sobrenome {get; set; } = string.Empty;
    public string Matricula { get; set; } = string.Empty;
    public string Departamento { get; set; } = string.Empty;
    public string Cargo { get; set; } = string.Empty;
}