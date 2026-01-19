namespace Ativos.Communication.Requests;

public class RequestUpdateUserJson
{
    public string P_Nome { get; set; } = string.Empty;
    public string Sobrenome { get; set; } = string.Empty;
    public long Matricula { get; set; }
}