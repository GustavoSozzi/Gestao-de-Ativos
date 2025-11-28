using Ativos.Communication.Requests;
using Ativos.Communication.responses;
using Ativos.Communication.responses.Chamados;
using Ativos.Communication.responses.Register;
using Ativos.Communication.responses.Usuarios;
using Ativos.Domain.Entities;
using AutoMapper;

namespace Ativos.Application.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestAtivosJson, Ativo>();
        CreateMap<RequestUsuariosJson, Usuario>() //ignora os dados password para criptografia da senha
            .ForMember(dest => dest.Password, config => config.Ignore());
        CreateMap<RequestChamadosJson, Chamado>();
        CreateMap<RequestContratosJson, Contrato>();
        CreateMap<RequestLicencasJson, Licenca>();
        CreateMap<RequestLocalizacaoJson, Localizacao>();
    }

    private void EntityToResponse()
    {
        CreateMap<Ativo, ResponseRegisterAtivosJson>();
        CreateMap<Ativo, ResponseShortAtivoJson>();
        CreateMap<Ativo, ResponseAtivosJson>();
        CreateMap<Ativo, ResponseAtivoJson>();
        CreateMap<Usuario, ResponseRegisterUsuariosJson>();
        CreateMap<Usuario, ResponseShortUsuarioJson>();
        CreateMap<Usuario, ResponseUsuarioJson>();
        CreateMap<Usuario, ResponseUsuariosJson>();
        CreateMap<Chamado, ResponseRegisterChamadosJson>();
        CreateMap<Chamado, ResponseChamadosJson>();
        CreateMap<Chamado, ResponseShortChamadoJson>();
        CreateMap<Contrato, ResponseRegisterContratosJson>();
        CreateMap<Licenca, ResponseRegisterLicencasJson>();
        CreateMap<Localizacao, ResponseRegisterLocalizacaoJson>();
        CreateMap<Localizacao, ResponseLocalizacaoJson>();
        CreateMap<Localizacao, ResponseShortLocalizacaoJson>();
    }
}