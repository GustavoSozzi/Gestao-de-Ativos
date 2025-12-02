namespace Ativos.Communication.Requests;

public class RequestVincularLicencaJson
{
    public long Id_Usuario { get; set; }
    public List<long> Ids_Licencas { get; set; } = [];
}
