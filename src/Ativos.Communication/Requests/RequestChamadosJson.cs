using Ativos.Communication.Enums;

namespace Ativos.Communication.Requests;

public class RequestChamadosJson
{
    public DateTime Data_Abertura { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descricao_Problema { get; set; } = string.Empty;
    public long Id_Ativo { get; set; }
    public StatusChamado Status_Chamado { get; set; }
}