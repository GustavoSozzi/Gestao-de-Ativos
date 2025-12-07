using Ativos.Communication.Requests;
using Bogus;

namespace CommonTestUtilities.Requests.Register;

public class RequestRegisterLocalizacaoJsonBuilder
{
    public static RequestLocalizacaoJson Build()
    {
        return new Faker<RequestLocalizacaoJson>()
            .RuleFor(r => r.Cidade, faker => faker.Address.City())
            .RuleFor(r => r.Estado, faker => faker.Address.StreetAddress());
    }
}