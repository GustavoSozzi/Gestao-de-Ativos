using Ativos.Application.UseCases.Reports.Pdf.Fonts;
using Ativos.Domain.Repositories.Chamados;
using MigraDoc.DocumentObjectModel;
using PdfSharp.Fonts;

namespace Ativos.Application.UseCases.Reports.Pdf;

public class GenerateChamadosReportPdfUseCase : IGenerateChamadosReportPdfUseCase
{
    private readonly IChamadosReadOnlyRepository _repository;
    
    public GenerateChamadosReportPdfUseCase(IChamadosReadOnlyRepository repository)
    {
        _repository = repository;

        GlobalFontSettings.FontResolver = new ChamadosReportFontResolver();
    }

    public async Task<byte[]> Execute(DateOnly month)
    {
        var chamados =  await _repository.FilterByMonth(month);
        if (chamados.Count == 0) return [];

        var document = CreateDocument(month);
        var page = CreatePage(document);

        var paragraph = page.AddParagraph();
        var title = string.Format("total de chamados em", month.ToString("Y"));

        paragraph.AddFormattedText(title, new Font { Name = FontHelper.RALEWAY_REGULAR, Size = 15 });
        
        paragraph.AddLineBreak();

        long total_chamados_abertos = chamados.Count();
        paragraph.AddFormattedText($"{total_chamados_abertos}", new Font { Name = FontHelper.WORKSANS_BLACK, Size = 50});

        return [];
    }

    private MigraDoc.DocumentObjectModel.Document CreateDocument(DateOnly month)
    {
        var document = new MigraDoc.DocumentObjectModel.Document();
        
        document.Info.Title = $"Chamados de {month:Y}";
        document.Info.Author = "Gustavo Sozzi";

        var style = document.Styles["Normal"];
        style!.Font.Name = FontHelper.RALEWAY_REGULAR;
        
        return document;
    }

    private Section CreatePage(MigraDoc.DocumentObjectModel.Document document)
    {
        var section = document.AddSection();
        section.PageSetup = document.DefaultPageSetup.Clone();
        
        section.PageSetup.PageFormat = PageFormat.A4;
        section.PageSetup.LeftMargin = 40;
        section.PageSetup.RightMargin = 40;
        section.PageSetup.TopMargin = 80;
        section.PageSetup.RightMargin = 80;

        return section;
    }
}