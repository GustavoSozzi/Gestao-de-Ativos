using Ativos.Communication.Enums;
using Ativos.Communication.Requests;
using Bogus;

namespace CommonTestUtilities.Requests.Register;

public class RequestRegisterLicencasJsonBuilder
{
    public static RequestLicencasJson Build()
    {
        return new Faker<RequestLicencasJson>()
            .RuleFor(r => r.Tipo_Licenca, faker => faker.PickRandom<TipoLicenca>())
            .RuleFor(r => r.Data, faker => faker.Date.Recent())
            .RuleFor(r => r.Nome_Soft, faker => faker.Commerce.ProductName());
    }
}