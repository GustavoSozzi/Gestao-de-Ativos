using Ativos.Domain.Entities;
using Bogus;

namespace CommonTestUtilities.Entities;

public class AtivosBuilder
{
    public static Ativo Build()
    {
        var ativos = new Faker<Ativo>()
            .RuleFor(r => r.Id_ativo, _ => 19)
            .RuleFor(r => r.id_localizacao, _ => 1)
            .RuleFor(r => r.Nome, faker => faker.Name.FullName())
            .RuleFor(r => r.Modelo, faker => faker.Lorem.Text())
            .RuleFor(r => r.SerialNumber, faker => faker.Random.String())
            .RuleFor(r => r.CodInventario, faker => faker.Random.Number(1,10000))
            .RuleFor(r => r.Tipo, faker => faker.Lorem.Text());

        return ativos;

    }
}