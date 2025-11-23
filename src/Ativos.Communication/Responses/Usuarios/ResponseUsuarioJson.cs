namespace Ativos.Communication.responses.Usuarios;

public class ResponseUsuarioJson
{
    public string P_nome { get; set; } = string.Empty;
    public string Sobrenome {get; set; } = string.Empty;
    public long Matricula { get; set; }
    public string Departamento { get; set; } = string.Empty;
    public string Cargo { get; set; } = string.Empty;
}