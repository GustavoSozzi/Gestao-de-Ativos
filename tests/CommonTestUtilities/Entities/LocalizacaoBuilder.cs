using Ativos.Domain.Entities;
using Bogus;

namespace CommonTestUtilities.Entities;

public class LocalizacaoBuilder
{
    public static Localizacao Build()
    {
        var localizacao = new Faker<Localizacao>()
            .RuleFor(r => r.Id_Localizacao, _ => 1)
            .RuleFor(r => r.Cidade, faker => faker.Address.City())
            .RuleFor(r => r.Estado, faker => faker.Address.State());

        return localizacao.Generate();
    }
}