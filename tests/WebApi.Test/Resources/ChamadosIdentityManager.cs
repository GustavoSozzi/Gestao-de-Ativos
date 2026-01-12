using Ativos.Domain.Entities;

namespace WebApi.Test.Resources;

public class ChamadosIdentityManager
{
    private Chamado _chamado;

    public ChamadosIdentityManager(Chamado chamado)
    {
        _chamado = chamado;
    }
    public long GetChamadoId() => _chamado.Id_Chamado;

    public DateTime GetDate() => _chamado.Data_Abertura;
}