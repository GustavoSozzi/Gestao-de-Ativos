using Ativos.Communication.Requests;
using Ativos.Domain.Entities;
using Ativos.Exception;
using FluentValidation;

namespace Ativos.Application.UseCases;

public class ChamadosValidator : AbstractValidator<RequestChamadosJson>
{
    public ChamadosValidator()
    {
        RuleFor(chamados => chamados.Data_Abertura).NotEmpty().WithMessage(ResourceErrorMessages.FIELD_REQUIRED)
            .Must(DateTimeIsValid).WithMessage("Data de abertura invalida");
        RuleFor(chamados => chamados.Titulo).NotEmpty().WithMessage(ResourceErrorMessages.FIELD_REQUIRED);
        RuleFor(chamados => chamados.Descricao_Problema).NotEmpty().WithMessage(ResourceErrorMessages.FIELD_REQUIRED);
        RuleFor(chamados => chamados.Status_Chamado).NotEmpty().WithMessage(ResourceErrorMessages.FIELD_REQUIRED).IsInEnum().WithMessage("Campo informado invalido");

    }

    private bool DateTimeIsValid(DateTime dataAbertura)
    {
        if (dataAbertura.Date <= DateTime.Now.Date)
        {
            return true;
        }

        return false;
    }
}