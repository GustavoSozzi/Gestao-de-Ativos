using Ativos.Communication.Requests;

namespace CommonTestUtilities.Requests.Login;
using Bogus;

public class RequestLoginJsonBuilder
{
    public static RequestLoginJson Build()
    {
        return new Faker<RequestLoginJson>()
            .RuleFor(user => user.Matricula, faker => faker.Random.Int(1, 100000))
            .RuleFor(user => user.Password, faker => faker.Internet.Password(prefix: "!Aa1"));
    }
}