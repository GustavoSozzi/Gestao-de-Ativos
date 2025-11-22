using Ativos.Communication.responses.Usuarios;

namespace Ativos.Communication.responses;

public class ResponseShortAtivoJson
{
    public long Id_ativo { get; set; }
    public long id_localizacao { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Modelo {get; set; } = string.Empty;
    public string SerialNumber { get; set; } = string.Empty;
    public long CodInventario { get; set; }
    public string? Tipo { get; set; }
    public long? id_usuario { get; set; }
    
    public ResponseShortLocalizacaoJson localizacao { get; set; }
    public ResponseShortUsuarioJson? Usuario { get; set; }
}