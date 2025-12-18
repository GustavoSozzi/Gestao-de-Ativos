using Ativos.Communication.Requests;

namespace CommonTestUtilities.Requests.Register;
using Bogus;

public class RequestVincularLicencaJsonBuilder
{
    public static RequestVincularLicencaJson Build()
    {
        return new Faker<RequestVincularLicencaJson>()
            .RuleFor(r => r.Id_Usuario, _ => 7)
            .RuleFor(r => r.Ids_Licencas, _ => new List<long> { 3 }); // Lista de Ids das licencas
    }
}