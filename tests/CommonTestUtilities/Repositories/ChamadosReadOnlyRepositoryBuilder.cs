using Ativos.Domain.Entities;
using Ativos.Domain.Repositories;
using Ativos.Domain.Repositories.Chamados;
using Moq;

namespace CommonTestUtilities.Repositories;

public class ChamadosReadOnlyRepositoryBuilder
{
    private readonly Mock<IAtivosReadOnlyRepository> _repository;

    public ChamadosReadOnlyRepositoryBuilder()
    {
        _repository = new Mock<IAtivosReadOnlyRepository>();
    }
    
    public ChamadosReadOnlyRepositoryBuilder WithGetByIdReturning(long id, Ativo ativo)
    {
        _repository
            .Setup(r => r.GetById(id))
            .ReturnsAsync(ativo);

        return this;
    }

    public IAtivosReadOnlyRepository Build() => _repository.Object;
}