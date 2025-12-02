namespace Ativos.Communication.responses.Register;

public class ResponseRegisterUsuariosLicencasJson
{
    public long Id_Usuario { get; set; }
    public List<long> Ids_Licencas { get; set; } = [];
}