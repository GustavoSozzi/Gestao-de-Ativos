using Ativos.Domain.Enums;
using Ativos.Domain.Entities;
using Bogus;

namespace CommonTestUtilities.Entities;

public class LicenceBuilder
{
    public static Licenca Build()
    {
        return new Faker<Licenca>()
            .RuleFor(r => r.Id_Licenca, _ => 3)
            .RuleFor(r => r.Tipo_Licenca, faker => faker.PickRandom<TipoLicenca>())
            .RuleFor(r => r.Data, faker => faker.Date.Recent());
    }

    public static Licenca BuildWithId(long id)
    {
        return new Faker<Licenca>()
            .RuleFor(r => r.Id_Licenca, _ => id)
            .RuleFor(r => r.Tipo_Licenca, faker => faker.PickRandom<TipoLicenca>())
            .RuleFor(r => r.Data, faker => faker.Date.Recent());
    }
}