namespace Ativos.Communication.Requests;

public class RequestContratosJson
{
    public string Tipo { get; set; } = string.Empty;
    public string Descricao {get; set; } = string.Empty;
    public Double Valor { get; set; }
    public long Id_Ativo {get; set; }
}