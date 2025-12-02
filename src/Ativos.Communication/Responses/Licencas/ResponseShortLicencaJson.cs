using Ativos.Communication.Enums;

namespace Ativos.Communication.responses;

public class ResponseShortLicencaJson
{
    public long Id_Licenca { get; set; }
    public TipoLicenca Tipo_Licenca { get; set; }
    public DateTime Data { get; set; }
}