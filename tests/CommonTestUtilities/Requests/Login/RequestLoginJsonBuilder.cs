using Ativos.Communication.Requests;

namespace CommonTestUtilities.Requests.Login;
using Bogus;

public class RequestLoginJsonBuilder
{
    public static RequestLoginJson Build()
    {
        return new Faker<RequestLoginJson>()
            .RuleFor(user => user.Matricula, _ => 5050)
            .RuleFor(user => user.Password, _ => "!Aa1password");
    }
}