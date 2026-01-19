using Ativos.Communication.Enums;

namespace Ativos.Communication.responses.Register;

public class ResponseRegisterChamadosJson
{
    public long Id_Chamado { get; set; }
    public IList<Tag> Tags { get; set; } = [];
}