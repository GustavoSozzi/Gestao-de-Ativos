using System.Net.Mime;
using Ativos.Application.UseCases.Reports.Excel;
using Microsoft.AspNetCore.Mvc;

namespace Ativos.Api.controllers;

public class ReportController : ControllerBase
{
    [HttpGet("excel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetExcel([FromServices] IGenerateChamadosReportExcelUseCase useCase, [FromHeader] DateOnly month)
    {
        byte[] file = await useCase.Execute(month);
        
        if(file.Length > 0) 
            return File(file, MediaTypeNames.Application.Octet, "report.xlsx");

        return NoContent();
    }
}

