using Ativos.Domain.Repositories.Chamados;
using Ativos.Domain.Services.LoggedUser;
using ClosedXML.Excel;

namespace Ativos.Application.UseCases.Reports.Excel;

public class GenerateChamadosReportExcelUseCase : IGenerateChamadosReportExcelUseCase
{
    private readonly IChamadosReadOnlyRepository _repository;
    private readonly ILoggedUser _loggedUser;

    public GenerateChamadosReportExcelUseCase(IChamadosReadOnlyRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<byte[]> Execute(DateOnly month)
    {
        var loggedUser = await _loggedUser.Get();
        
        var chamados = await _repository.FilterByMonth(loggedUser, month);

        if (chamados.Count == 0) return [];
        
        var workbook = new XLWorkbook();

        workbook.Author = loggedUser.P_nome;
        workbook.Style.Font.FontSize = 12;
        workbook.Style.Font.FontName = "Times New Roman";
        
        var worksheet = workbook.Worksheets.Add(month.ToString("Y"));
        
        InsertHeader(worksheet);

        var row = 2;
        foreach (var chamado in chamados)
        {
            worksheet.Cell($"A{row}").Value = chamado.Titulo;
            worksheet.Cell($"B{row}").Value = chamado.Descricao;
            worksheet.Cell($"C{row}").Value = chamado.Solucao;
            worksheet.Cell($"D{row}").Value = chamado.Status_Chamado.ToString();  //retorna string como ex: "aberto", "andamento", "fechado";
            worksheet.Cell($"E{row}").Value = chamado.Data_Abertura;

            row++;
        }
        
        worksheet.Columns().AdjustToContents();
        
        var file = new MemoryStream();
        workbook.SaveAs(file);
        
        return file.ToArray();
    }

    private void InsertHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell("A1").Value = "Titulo";
        worksheet.Cell("B1").Value = "Descricao";
        worksheet.Cell("C1").Value = "Solucao";
        worksheet.Cell("D1").Value = "Status chamado";
        worksheet.Cell("E1").Value = "Data abertura";
        
        worksheet.Cells("A1:E1").Style.Font.Bold = true; //todos os titulos como negrito
        
        worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("#F5C2B6");

        worksheet.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
        worksheet.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
    }
}