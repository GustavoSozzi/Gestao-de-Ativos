namespace Ativos.Communication.responses;

public class ResponseAtivoJson
{
    public string Nome { get; set; } = string.Empty;
    public string Modelo {get; set; } = string.Empty;
    public string SerialNumber { get; set; } = string.Empty;
    public long CodInventario { get; set; }
    public string? Tipo { get; set; }
}