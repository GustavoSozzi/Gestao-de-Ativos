using Ativos.Communication.Requests;
using Bogus;

namespace CommonTestUtilities.Requests.Update;

public class RequestUpdateAtivosJsonBuilder
{
    public static RequestAtivosJson Build()
    {
        return new Faker<RequestAtivosJson>()
            .RuleFor(r => r.Nome, faker => faker.Name.FullName(Bogus.DataSets.Name.Gender.Male))
            .RuleFor(r => r.Modelo, faker => faker.Commerce.Product())
            .RuleFor(r => r.SerialNumber, faker => faker.Random.AlphaNumeric(10))
            .RuleFor(r => r.CodInventario, faker => faker.Random.Number(1,10000))
            .RuleFor(r => r.Tipo, faker => faker.Random.String(10));
    }
}