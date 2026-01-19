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
        CreateMap<RequestVincularLicencaJson, Licenca>();
        CreateMap<RequestContratosJson, Contrato>();
        CreateMap<RequestLicencasJson, Licenca>();
        CreateMap<RequestLocalizacaoJson, Localizacao>();
        CreateMap<RequestChamadosJson, Chamado>().ForMember(dest => dest.Tags,
            config => config.MapFrom(source => source.Tags.Distinct()));
        CreateMap<Ativos.Communication.Enums.Tag, Tag>()
            .ForMember(dest => dest.Value, config => config.MapFrom(source => source));
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
        CreateMap<Usuario, ResponseUserProfileJson>();
        CreateMap<Usuario, ResponseRegisterUsuariosLicencasJson>()
            .ForMember(dest => dest.Ids_Licencas, opt => opt.MapFrom(src => src.licencas.Select(l => l.Id_Licenca).ToList()));
        CreateMap<Usuario, ResponseAtivosJson>();
        CreateMap<Chamado, ResponseRegisterChamadosJson>()
            .ForMember(dest => dest.Tags, config => config.MapFrom(source => source.Tags.Select(tag => tag.Value)));
        CreateMap<Chamado, ResponseChamadosJson>();
        CreateMap<Chamado, ResponseShortChamadoJson>();
        CreateMap<Contrato, ResponseRegisterContratosJson>();
        CreateMap<Licenca, ResponseRegisterLicencasJson>();
        CreateMap<Licenca, ResponseLicencasJson >();
        CreateMap<Licenca, ResponseShortLicencaJson>();
        CreateMap<Localizacao, ResponseRegisterLocalizacaoJson>();
        CreateMap<Localizacao, ResponseLocalizacaoJson>();
        CreateMap<Localizacao, ResponseShortLocalizacaoJson>();
    }
}