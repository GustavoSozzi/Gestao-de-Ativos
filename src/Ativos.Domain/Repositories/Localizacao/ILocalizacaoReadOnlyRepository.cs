namespace Ativos.Domain.Repositories.Localizacao;

public interface ILocalizacaoReadOnlyRepository
{
    Task<List<Entities.Localizacao>> GetAll();
}