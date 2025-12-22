namespace Ativos.Application.UseCases.Reports.Excel;

public interface IGenerateChamadosReportExcelUseCase
{
    Task<byte[]> Execute(DateOnly month);
}