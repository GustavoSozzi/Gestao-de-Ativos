namespace Ativos.Application.UseCases.Reports.Pdf;

public interface IGenerateChamadosReportPdfUseCase
{
    Task<byte[]> Execute(DateOnly month);
}