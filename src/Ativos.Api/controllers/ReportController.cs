using System.Net.Mime;
using Ativos.Application.UseCases.Reports.Excel;
using Ativos.Application.UseCases.Reports.Pdf;
using Microsoft.AspNetCore.Mvc;

namespace Ativos.Api.controllers;

public class ReportController : ControllerBase
{
    [HttpGet("excel/chamados")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetExcel([FromServices] IGenerateChamadosReportExcelUseCase useCase, [FromHeader] DateOnly month)
    {
        byte[] file = await useCase.Execute(month);
        
        if(file.Length > 0) 
            return File(file, MediaTypeNames.Application.Octet, "report_chamados.xlsx");

        return NoContent();
    }
    
    [HttpGet("excel/ativos")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetExcel([FromServices] IGenerateAtivosReportExcelUseCase useCase)
    {
        byte[] file = await useCase.Execute();
        
        if(file.Length > 0) 
            return File(file, MediaTypeNames.Application.Octet, "report_ativos.xlsx");

        return NoContent();
    }
    
    [HttpGet("pdf/chamados")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetPdf([FromServices] IGenerateChamadosReportPdfUseCase useCase,  [FromQuery] DateOnly month)
    {
        byte[] file = await useCase.Execute(month);
        
        if(file.Length > 0) 
            return File(file, MediaTypeNames.Application.Pdf, "report_chamados.pdf");

        return NoContent();
    }
}

