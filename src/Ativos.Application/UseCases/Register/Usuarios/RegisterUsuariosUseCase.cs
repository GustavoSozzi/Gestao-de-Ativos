using Ativos.Communication.Requests;
using Ativos.Communication.responses.Register;
using Ativos.Domain;
using Ativos.Domain.Entities;
using Ativos.Domain.Repositories;
using Ativos.Domain.Repositories.Usuarios;
using Ativos.Domain.Security.Cryptography;
using Ativos.Domain.Security.Tokens;
using Ativos.Exception.ExceptionsBase;
using AutoMapper;
using FluentValidation.Results;

namespace Ativos.Application.UseCases.Register.Usuarios;

public class RegisterUsuariosUseCase : IRegisterUsuariosUseCase
{
    private readonly IUsuariosWriteOnlyRepository _repository;
    private readonly IUsuariosReadOnlyRepository _userReadOnlyRepository;
    private readonly IAccessTokenGenerator _tokenGenerator;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterUsuariosUseCase(IUsuariosWriteOnlyRepository repository,
        IAccessTokenGenerator tokenGenerator,
        IUsuariosReadOnlyRepository userReadOnlyRepository, 
        IPasswordEncripter passwordEncripter, 
        IUnitOfWork unitOfWork, 
        IMapper mapper)
    {
        _repository = repository;
        _userReadOnlyRepository = userReadOnlyRepository;
        _tokenGenerator = tokenGenerator;
        _passwordEncripter = passwordEncripter;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseRegisterUsuariosJson> Execute(RequestUsuariosJson request)
    {
        await ValidateUsuarios(request);

        var entity = _mapper.Map<Usuario>(request);
        entity.Password = _passwordEncripter.Encrypt(request.Password); //criptografando a senha
        entity.UserIdentifier = Guid.NewGuid();
        
        await _repository.Add(entity);
        
        await _unitOfWork.Commit();
        
        var response = _mapper.Map<ResponseRegisterUsuariosJson>(entity);
        response.Token = _tokenGenerator.Generate(entity);
        
        return response;
    }

    private async Task ValidateUsuarios(RequestUsuariosJson request)
    {
        var result = new UsuariosValidator().Validate(request);
        
        var matriculaExist = await _userReadOnlyRepository.ExistActiveUserMatricula(request.Matricula); //verificando se ja existe a matricula
        if (matriculaExist)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, "Matricula ja registrada"));
        }
        
        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}