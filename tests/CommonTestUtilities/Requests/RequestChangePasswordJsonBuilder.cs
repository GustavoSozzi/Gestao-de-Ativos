using Ativos.Communication.Requests;
using Ativos.Domain.Entities;
using Bogus;

namespace CommonTestUtilities.Requests;

public class RequestChangePasswordJsonBuilder
{
    public static RequestChangePasswordJson Build()
    {
        return new Faker<RequestChangePasswordJson>()
            .RuleFor(usuario => usuario.Password, faker => faker.Internet.Password())
            .RuleFor(usuario => usuario.NewPassword, faker => faker.Internet.Password(prefix: "!Aa1"));
    }
}