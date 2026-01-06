using Ativos.Domain.Entities;

namespace WebApi.Test.Resources;

public class UserIdentityManager
{
    private readonly Usuario _user;
    private string _password;
    private string _token;

    public UserIdentityManager(Usuario usuario, string password, string token) {
        _user = usuario;
        _password = password;
        _token = token;
    }
    
    public string GetFirstName() => _user.P_nome;
    public string GetLastName() => _user.Sobrenome;
    public long GetMatricula() => _user.Matricula;
    public string GetToken() => _token;
    public string GetPassword() => _password;
    public long GetUsuarioId() => _user.Id_usuario;
}