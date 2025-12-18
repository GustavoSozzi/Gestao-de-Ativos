using Ativos.Domain.Entities;
using CommonTestUtilities.Cryptography;
using Bogus;

namespace CommonTestUtilities.Entities;

public class UserBuilder
{
    public static Usuario Build()
    {
        var passwordEncripter = PasswordEncripterBuilder.Build();

        var user = new Faker<Usuario>()
            .RuleFor(u => u.Id_usuario, _ => 7)
            .RuleFor(r => r.P_nome, faker => faker.Name.FirstName(Bogus.DataSets.Name.Gender.Male))
            .RuleFor(r => r.Sobrenome, faker => faker.Name.LastName())
            .RuleFor(r => r.Cargo, faker => faker.Random.String(10))
            .RuleFor(r => r.Departamento, faker => faker.Random.String(10))
            .RuleFor(r => r.Matricula, _ => 5050)
            .RuleFor(r => r.Password, _ => passwordEncripter.Encrypt("!Aa1password"))
            .RuleFor(r => r.UserIdentifier, _ => Guid.NewGuid());

        return user;
    }
}