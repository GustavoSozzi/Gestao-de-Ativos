namespace Ativos.Communication.responses;

public class ResponseShortLocalizacaoJson
{
    public long id_localizacao {get; set;}
    public string Cidade { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
}