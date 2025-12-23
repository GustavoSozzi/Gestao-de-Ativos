namespace Ativos.Application.UseCases.Reports.Excel;

public interface IGenerateAtivosReportExcelUseCase
{
    Task<byte[]> Execute();
}