using Ativos.Communication.Requests;
using Bogus;

namespace CommonTestUtilities.Requests.Update;

public class RequestUpdateUserJsonBuilder
{
    public static RequestUpdateUserJson Build()
    {
        return new Faker<RequestUpdateUserJson>()
            .RuleFor(r => r.P_Nome, faker => faker.Name.FirstName(Bogus.DataSets.Name.Gender.Male))
            .RuleFor(r => r.Sobrenome, faker => faker.Name.LastName())
            .RuleFor(r => r.Matricula, faker => faker.Random.Int(1, 100000));
    }
}