namespace Ativos.Communication.responses.Usuarios;

public class ResponseShortUsuarioJson
{
    public long Id_usuario { get; set; }
    public string P_nome { get; set; } = string.Empty;
    public string Sobrenome {get; set; } = string.Empty;
    public long Matricula { get; set; }
}