using Ativos.Domain.Repositories;
using Ativos.Domain.Services.LoggedUser;
using ClosedXML.Excel;

namespace Ativos.Application.UseCases.Reports.Excel;

public class GenerateAtivosReportExcelUseCase : IGenerateAtivosReportExcelUseCase
{
    private readonly IAtivosReadOnlyRepository _repository;
    private readonly ILoggedUser _loggedUser;

    public GenerateAtivosReportExcelUseCase(IAtivosReadOnlyRepository repository, ILoggedUser loggedUser)
    {
        _repository = repository;
    }

    public async Task<byte[]> Execute()
    {
        var loggedUser = await _loggedUser.Get();
        
        var ativos = await _repository.GetAll(loggedUser);

        if (ativos.Count == 0) return [];

        var workbook = new XLWorkbook();
        
        workbook.Author = "Gustavo Sozzi Bom";
        workbook.Style.Font.FontSize = 12;
        workbook.Style.Font.FontName = "Times New Roman";
        
        var worksheet = workbook.AddWorksheet("Ativos");
        
        InsertHeader(worksheet);
        
        var row = 2;
        foreach (var ativo in ativos)
        {
            worksheet.Cell($"A{row}").Value = ativo.Nome;
            worksheet.Cell($"B{row}").Value = ativo.Modelo;
            worksheet.Cell($"C{row}").Value = ativo.SerialNumber;
            worksheet.Cell($"D{row}").Value = ativo.CodInventario;  //retorna string como ex: "aberto", "andamento", "fechado";
            worksheet.Cell($"E{row}").Value = ativo.Tipo;

            row++;
        }
        
        worksheet.Columns().AdjustToContents();
        
        var file = new MemoryStream();
        workbook.SaveAs(file);
        
        return file.ToArray();
    }
    
    
    private void InsertHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell("A1").Value = "Nome";
        worksheet.Cell("B1").Value = "Modelo";
        worksheet.Cell("C1").Value = "Serial Number";
        worksheet.Cell("D1").Value = "Codigo inventario";
        worksheet.Cell("E1").Value = "Tipo";
        
        worksheet.Cells("A1:E1").Style.Font.Bold = true; //todos os titulos como negrito
        
        worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("#F5C2B6");

        worksheet.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
        worksheet.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
    }
}