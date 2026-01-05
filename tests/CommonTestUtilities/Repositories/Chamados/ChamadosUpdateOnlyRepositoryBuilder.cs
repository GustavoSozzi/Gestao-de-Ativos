using Ativos.Domain.Entities;
using Ativos.Domain.Repositories.Chamados;
using Moq;

namespace CommonTestUtilities.Repositories;

public class ChamadosUpdateOnlyRepositoryBuilder
{
    private readonly Mock<IChamadosUpdateOnlyRepository> _repository;
    
    public ChamadosUpdateOnlyRepositoryBuilder() => _repository = new Mock<IChamadosUpdateOnlyRepository>();
    
    public ChamadosUpdateOnlyRepositoryBuilder GetById(Usuario usuario, Chamado? chamado)
    {
        _repository.Setup(chamadoRepository => chamadoRepository.GetById(usuario, chamado.Id_Chamado)).ReturnsAsync(chamado);

        return this;
    }

    public IChamadosUpdateOnlyRepository Build() => _repository.Object;
    
}