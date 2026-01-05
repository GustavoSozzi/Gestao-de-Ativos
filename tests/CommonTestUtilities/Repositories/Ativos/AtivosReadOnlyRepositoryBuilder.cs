using Ativos.Domain.Entities;
using Ativos.Domain.Repositories;
using Moq;

namespace CommonTestUtilities.Repositories;

public class AtivosReadOnlyRepositoryBuilder
{
    private readonly Mock<IAtivosReadOnlyRepository> _repository;
    
    public AtivosReadOnlyRepositoryBuilder()
    {
        _repository = new Mock<IAtivosReadOnlyRepository>();
    }

    public AtivosReadOnlyRepositoryBuilder GetAll(Usuario usuario, List<Ativo> ativos)
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
        )).ReturnsAsync(ativos);

        return this;
    }

    public AtivosReadOnlyRepositoryBuilder GetById(Usuario usuario, Ativo? ativo)
    {
        if(ativo is not null) _repository.Setup(repository => repository.GetById(usuario, ativo.Id_ativo)).ReturnsAsync(ativo);

        return this;
    }

    public AtivosReadOnlyRepositoryBuilder GetAllWithFilters(Usuario usuario, List<Ativo> ativos, 
        string? nome = null, string? modelo = null, string? tipo = null, 
        long? codInventario = null, string? cidade = null, string? estado = null, 
        long? matriculaUsuario = null, string? nomeUsuario = null)
    {
        _repository.Setup(repository => repository.GetAll(
            usuario, nome, modelo, tipo, codInventario, cidade, estado, matriculaUsuario, nomeUsuario
        )).ReturnsAsync(ativos);

        return this;
    }

    public IAtivosReadOnlyRepository Build() => _repository.Object;
}