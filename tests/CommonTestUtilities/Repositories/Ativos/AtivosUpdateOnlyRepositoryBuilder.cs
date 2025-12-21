using Ativos.Domain.Entities;
using Moq;

namespace CommonTestUtilities.Repositories;

public class AtivosUpdateOnlyRepositoryBuilder
{
    private readonly Mock<IAtivosUpdateOnlyRepository> _repository;

    public AtivosUpdateOnlyRepositoryBuilder() => _repository = new Mock<IAtivosUpdateOnlyRepository>();

    public AtivosUpdateOnlyRepositoryBuilder GetById(Ativo? ativos)
    {
        _repository.Setup(ativoRepository => ativoRepository.GetById(ativos.Id_ativo)).ReturnsAsync(ativos);

        return this;
    }

    public IAtivosUpdateOnlyRepository Build() => _repository.Object;
}