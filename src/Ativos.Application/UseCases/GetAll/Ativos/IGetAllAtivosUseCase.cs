using Ativos.Communication.responses;

namespace Ativos.Application.UseCases.GetAll;

public interface IGetAllAtivosUseCase
{
    Task<ResponseAtivosJson> Execute(string? nome = null, string? modelo = null, string? tipo = null, 
        long? codInventario = null, string? cidade = null, string? estado = null, 
        long? matriculaUsuario = null, string? nomeUsuario = null);
}