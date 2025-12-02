using Ativos.Communication.Enums;

namespace Ativos.Communication.Requests;

public class RequestLicencasJson
{
    public string Nome_Soft { get; set; } = string.Empty;
    
    public TipoLicenca Tipo_Licenca { get; set; }
    public DateTime Data {get; set;}
    
    public long id_usuario { get; set; }
}