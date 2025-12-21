using Ativos.Domain.Entities;
using Ativos.Domain.Enums;
using Bogus;

namespace CommonTestUtilities.Entities;

public class ChamadosBuilder
{
    public static Chamado Build()
    {
        var chamados = new Faker<Chamado>()
            .RuleFor(r => r.Id_Chamado, _ => 2)
            .RuleFor(r => r.Data_Abertura, faker => faker.Date.Recent())
            .RuleFor(r => r.Titulo, faker => faker.Random.String(10))
            .RuleFor(r => r.Descricao, faker => faker.Lorem.Sentence(3, 10))
            .RuleFor(r => r.Status_Chamado, faker => faker.PickRandom<StatusChamado>())
            .RuleFor(r => r.id_Ativo, _ => 19);
            
        return chamados;
    }
}