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
    
    public ChamadosReadOnlyRepositoryBuilder GetById(Chamado? chamado)
    {
        Ativo ativo = new Ativo();
        
        _repository
            .Setup(r => r.GetById(chamado.id_Ativo))
            .ReturnsAsync(ativo);

        return this;
    }

    public IAtivosReadOnlyRepository Build() => _repository.Object;
}