using Ativos.Communication.Enums;

namespace Ativos.Communication.responses.Register;

public class ResponseRegisterLicencasJson
{
    public string Nome_Soft { get; set; } = string.Empty;
    public TipoLicenca Tipo_Licenca { get; set; }
}