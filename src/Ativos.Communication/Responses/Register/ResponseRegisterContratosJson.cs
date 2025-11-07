namespace Ativos.Communication.responses.Register;

public class ResponseRegisterContratosJson
{
    public long Id_Contrato { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public string Descricao {get; set; } = string.Empty;
    public Double Valor { get; set; }
}