using Ativos.Communication.Requests;
using Ativos.Communication.responses.Register;
using Ativos.Domain;
using Ativos.Domain.Repositories.Licencas;
using Ativos.Domain.Repositories.Usuarios;
using AutoMapper;

namespace Ativos.Application.UseCases.Register.Usuarios;

public class RegisterUsuariosLicencasUseCase : IRegisterUsuariosLicencasUseCase
{
    
    private readonly IUsuariosReadOnlyRepository _usuariosReadOnlyRepository;
    private readonly ILicencasReadOnlyRepository _licencasReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public RegisterUsuariosLicencasUseCase(IUsuariosReadOnlyRepository usuarioRepository, ILicencasReadOnlyRepository licencaRepository,
        IUnitOfWork unitOfWork, 
        IMapper mapper)
    {
        _usuariosReadOnlyRepository = usuarioRepository;
        _licencasReadOnlyRepository = licencaRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<ResponseRegisterUsuariosLicencasJson> Execute(RequestVincularLicencaJson request)
    {
        var user = await _usuariosReadOnlyRepository.GetById(request.Id_Usuario);

        if (user == null) throw new System.Exception("User not found");

        foreach (var idLicenca in request.Ids_Licencas) //passando a lista como parametro no for
        {
            if (user.licencas.Any(l => l.Id_Licenca == idLicenca)) //verifica se o usuario ja possui a licenca
                continue;
            
            var licenca = await _licencasReadOnlyRepository.GetById(idLicenca);
            if (licenca == null) throw new System.Exception($"license {idLicenca} not found");
            user.licencas.Add(licenca);
        }
        
        await _unitOfWork.Commit();
    
        var response = _mapper.Map<ResponseRegisterUsuariosLicencasJson>(user);
        return response;
    }
}