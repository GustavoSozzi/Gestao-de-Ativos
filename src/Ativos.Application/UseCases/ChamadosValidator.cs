using Ativos.Communication.Requests;
using Ativos.Domain.Entities;
using Ativos.Exception;
using FluentValidation;

namespace Ativos.Application.UseCases;

public class ChamadosValidator : AbstractValidator<RequestChamadosJson>
{
    public ChamadosValidator()
    {
        RuleFor(chamados => chamados.Data_Abertura).Must(DateTimeIsValid).WithMessage("Data de abertura invalida");
        RuleFor(chamados => chamados.Titulo).NotEmpty().WithMessage(ResourceErrorMessages.FIELD_REQUIRED);
        RuleFor(chamados => chamados.Descricao).NotEmpty().WithMessage(ResourceErrorMessages.FIELD_REQUIRED);
        RuleFor(chamados => chamados.Status_Chamado).NotEmpty().WithMessage(ResourceErrorMessages.FIELD_REQUIRED).IsInEnum().WithMessage("Campo informado invalido");
        RuleFor(chamados => chamados.Id_Ativo).GreaterThan(0).WithMessage("Ativo é obrigatório");
    }

    private bool DateTimeIsValid(DateTime dataAbertura)
    {
        var pastLimit = DateTime.UtcNow.AddMonths(-1);
        
        if (dataAbertura.Date > DateTime.Now.Date) return false;
        if (dataAbertura.Date <  pastLimit) return false;

        return true;
    }
}