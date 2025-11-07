using Ativos.Domain.Entities;

namespace Ativos.Domain.Repositories.Localizacao;

public interface ILocalizacaoWriteOnlyRepository
{
    public Task Add(Entities.Localizacao localizacao);
}