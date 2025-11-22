using Ativos.Communication.Requests;
using Ativos.Communication.responses.Register;
using Ativos.Domain.Repositories.Usuarios;
using Ativos.Domain.Security.Cryptography;
using Ativos.Domain.Security.Tokens;
using Ativos.Exception.ExceptionsBase;
using AutoMapper;

namespace Ativos.Application.UseCases.Login.DoLogin;

public class DoLoginUseCase : IDoLoginUseCase
{
    private readonly IUsuariosReadOnlyRepository _repository;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IAccessTokenGenerator  _accessTokenGenerator;
    private readonly IMapper _mapper;

    public DoLoginUseCase(IUsuariosReadOnlyRepository repository,
        IPasswordEncripter passwordEncripter,
        IAccessTokenGenerator accessTokenGenerator,
        IMapper mapper)
    {
        _passwordEncripter = passwordEncripter;
        _repository = repository;
        _accessTokenGenerator = accessTokenGenerator;
        _mapper = mapper;
    }

    public async Task<ResponseRegisterUsuariosJson> Execute(RequestLoginJson request)
    {
        var user = await _repository.GetUserByMatricula(request.Matricula);
        if (user is null) { throw new InvalidLoginException();}
        
        var passwordMatch= _passwordEncripter.Verify(request.Password, user.Password);

        if (!passwordMatch) {throw new InvalidLoginException();}

        var response = _mapper.Map<ResponseRegisterUsuariosJson>(user);
        response.Token = _accessTokenGenerator.Generate(user);
        
        return response;
    }
}