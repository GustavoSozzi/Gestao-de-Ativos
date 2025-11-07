namespace Ativos.Communication.Requests;

public class RequestAtivosJson
{
    public string Nome { get; set; } = string.Empty;
    public string Modelo {get; set; } = string.Empty;
    public string SerialNumber { get; set; } = string.Empty;
    public long CodInventario { get; set; }
    public string? Tipo { get; set; }

    
    public long id_localizacao { get; set; }
}