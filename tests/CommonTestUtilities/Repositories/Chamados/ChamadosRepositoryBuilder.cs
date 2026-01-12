using Ativos.Domain.Entities;
using Ativos.Domain.Repositories.Chamados;
using Moq;

namespace CommonTestUtilities.Repositories;

public class ChamadosRepositoryBuilder
{
    private readonly Mock<IChamadosReadOnlyRepository> _repository;

    public ChamadosRepositoryBuilder()
    {
        _repository = new Mock<IChamadosReadOnlyRepository>();
    }
    
    public ChamadosRepositoryBuilder GetAll(Usuario usuario, List<Chamado> chamados)
    {
        _repository.Setup(repository => repository.GetAll(
            usuario, 
            It.IsAny<string?>(),
            It.IsAny<string?>(),
            It.IsAny<string?>(),
            It.IsAny<long?>(),
            It.IsAny<string?>(), 
            It.IsAny<string?>(),
            It.IsAny<long?>(),  
            It.IsAny<string?>()  
        )).ReturnsAsync(chamados);

        return this;
    }

    public ChamadosRepositoryBuilder FilterByMonth(Usuario usuario, List<Chamado> chamados)
    {
        _repository.Setup(repository => repository.FilterByMonth(usuario, It.IsAny<DateOnly>())).ReturnsAsync(chamados);

        return this;
    }
    public IChamadosReadOnlyRepository Build() => _repository.Object;

}