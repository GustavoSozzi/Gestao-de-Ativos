namespace Ativos.Communication.Requests;

public class RequestLicencasJson
{
    public string Nome_Soft { get; set; } = string.Empty;
    public string Tipo {get; set;} = string.Empty;
    public DateTime Data {get; set;}
}