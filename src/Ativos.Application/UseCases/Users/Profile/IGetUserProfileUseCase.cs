using Ativos.Communication.responses.Usuarios;


namespace Ativos.Application.UseCases.Users.Profile;

public interface IGetUserProfileUseCase
{
    Task<ResponseUserProfileJson> Execute();
}