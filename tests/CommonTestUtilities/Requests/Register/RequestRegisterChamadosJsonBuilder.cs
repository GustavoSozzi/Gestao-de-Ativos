using Ativos.Communication.Enums;
using Ativos.Communication.Requests;
using Bogus;

namespace CommonTestUtilities.Requests.Register;

public class RequestRegisterChamadosJsonBuilder
{
    public static RequestChamadosJson Build()
    {
        return new Faker<RequestChamadosJson>()
            .RuleFor(r => r.Data_Abertura, faker => faker.Date.Recent())
            .RuleFor(r => r.Titulo, faker => faker.Random.String(10))
            .RuleFor(r => r.Descricao, faker => faker.Lorem.Sentence(3, 10))
            .RuleFor(r => r.Status_Chamado, faker => faker.PickRandom<StatusChamado>())
            .RuleFor(r => r.Id_Ativo, faker => faker.Random.Int(1, 10000));
    }
}