using Ativos.Application.UseCases.Reports.Pdf.Colors;
using Ativos.Application.UseCases.Reports.Pdf.Fonts;
using Ativos.Domain.Repositories.Chamados;
using Ativos.Domain.Services.LoggedUser;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;

namespace Ativos.Application.UseCases.Reports.Pdf;

public class GenerateChamadosReportPdfUseCase : IGenerateChamadosReportPdfUseCase
{
    private readonly IChamadosReadOnlyRepository _repository;
    
    private readonly ILoggedUser _loggedUser;
    private const int HEIGHT_ROW_CALLED_TABLE = 25; 
    
    public GenerateChamadosReportPdfUseCase(IChamadosReadOnlyRepository repository, ILoggedUser loggedUser)
    {
        _repository = repository;
        _loggedUser = loggedUser;
        
        GlobalFontSettings.FontResolver = new ChamadosReportFontResolver();
    }

    public async Task<byte[]> Execute(DateOnly month)
    {
        var loggedUser = await _loggedUser.Get();
        
        var chamados =  await _repository.FilterByMonth(loggedUser, month);
        if (chamados.Count == 0) return [];

        var document = CreateDocument(loggedUser.P_nome, month);
        var page = CreatePage(document);

        CreateHeaderWithProfilePhotoAndName(loggedUser.P_nome, page);
        long total_open_calls = chamados.Count();
        CreateTotalCalledSection(page, month, total_open_calls);

        foreach (var chamado in chamados)
        {
            var table = CreateCalledTable(page);

            var row = table.AddRow();
            row.Height = HEIGHT_ROW_CALLED_TABLE; //altura da linha

            AddCalledTitle(row.Cells[0], chamado.Titulo);
            
            AddHeaderForDate(row.Cells[3]);

            row = table.AddRow();
            row.Height = HEIGHT_ROW_CALLED_TABLE;

            row.Cells[0].AddParagraph(chamado.Descricao);
            SetStyleBaseForCalledInformation(row.Cells[0]);
            row.Cells[0].Format.LeftIndent = 20;
            
            row.Cells[1].AddParagraph(chamado.Data_Abertura.ToString("t"));
            SetStyleBaseForCalledInformation(row.Cells[1]);
            
            row.Cells[2].AddParagraph(chamado.Status_Chamado.ToString()); //indice 2
            SetStyleBaseForCalledInformation(row.Cells[2]);
            
            AddDateForCalled(row.Cells[3], chamado.Data_Abertura);
            
            if(string.IsNullOrWhiteSpace(chamado.Solucao) == false)
            {
                var solutionRow = table.AddRow();
                solutionRow.Height = HEIGHT_ROW_CALLED_TABLE;
                
                solutionRow.Cells[0].AddParagraph(chamado.Solucao);
                
                solutionRow.Cells[0].Format.Font = new Font { Name = FontHelper.WORKSANS_REGULAR, Size = 10, Color = ColorsHelper.BLACK }; //estilizando a fonte titulo na linha
                solutionRow.Cells[0].Shading.Color = ColorsHelper.GREEN_LIGHT; //cor de fundo do titulo na linha
                solutionRow.Cells[0].VerticalAlignment = VerticalAlignment.Center; //alinhando a celula no centro
                solutionRow.Cells[0].MergeRight = 2; //mesclando com outras duas colunas a direita
                solutionRow.Cells[0].Format.LeftIndent = 20;

                row.Cells[3].MergeDown = 1;
            }
            
            AddWhiteSpace(table);
        }
        
        return RenderDocument(document);
    }

    private MigraDoc.DocumentObjectModel.Document CreateDocument(string author, DateOnly month)
    {
        var document = new MigraDoc.DocumentObjectModel.Document();
        
        document.Info.Title = $"Chamados de {month:Y}";
        document.Info.Author = author;

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
        section.PageSetup.BottomMargin = 80;

        return section;
    }

    private void CreateHeaderWithProfilePhotoAndName(string name, Section page)
    {
        var table = page.AddTable();
        table.AddColumn();
        table.AddColumn("300");

        var row = table.AddRow();
        row.Cells[0].AddImage("/Users/gustavosozzibom/Documents/Ufgd/Projeto banco de dados/GestaoDeAtivosApi/foto-tecnologia.png");

        row.Cells[1].AddParagraph($"Hey, {name}");
        row.Cells[1].Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 16 };
        row.Cells[1].VerticalAlignment = VerticalAlignment.Center; //alinhando a coluna ao centro
    }

    private void CreateTotalCalledSection(Section page, DateOnly month, long totalCalled)
    {
        var paragraph = page.AddParagraph();
        
        paragraph.Format.SpaceBefore = "40";
        paragraph.Format.SpaceAfter = "40";
        
        var title = string.Format($"total de chamados em: {month.ToString("Y")}");

        paragraph.AddFormattedText(title, new Font { Name = FontHelper.RALEWAY_REGULAR, Size = 15 });
        
        paragraph.AddLineBreak();
        
        paragraph.AddFormattedText($"{totalCalled}", new Font { Name = FontHelper.WORKSANS_BLACK, Size = 50});
    }

    private Table CreateCalledTable(Section page) //criar tabelas para os chamados registrados
    {
        var table = page.AddTable();
        
        table.AddColumn("195").Format.Alignment = ParagraphAlignment.Left;
        table.AddColumn("80").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Right;

        return table;
    }

    private void AddCalledTitle(Cell cells, string calledTitle)
    {
        cells.AddParagraph(calledTitle);
        cells.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorsHelper.BLACK }; //estilizando a fonte titulo na linha
        cells.Shading.Color = ColorsHelper.RED_LIGHT; //cor de fundo do titulo na linha
        cells.VerticalAlignment = VerticalAlignment.Center; //alinhando a celula no centro
        cells.MergeRight = 2; //mesclando com outras duas colunas a direita
        cells.Format.LeftIndent = 20;
    }

    private void AddHeaderForDate(Cell cell)
    {
        cell.AddParagraph("Data");
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorsHelper.WHITE};
        cell.Shading.Color = ColorsHelper.RED_DARK; 
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private void SetStyleBaseForCalledInformation(Cell cell)
    {
        cell.Format.Font = new Font { Name = FontHelper.WORKSANS_REGULAR, Size = 12, Color = ColorsHelper.BLACK};
        cell.Shading.Color = ColorsHelper.GREEN_DARK;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private void AddDateForCalled(Cell cell, DateTime date)
    {
        cell.AddParagraph(date.ToString("MM/dd/yyyy"));
        cell.Format.Font = new Font { Name = FontHelper.WORKSANS_REGULAR, Size = 14, Color = ColorsHelper.BLACK};
        cell.Shading.Color = ColorsHelper.WHITE;
        cell.VerticalAlignment = VerticalAlignment.Center;   
    }

    private void AddWhiteSpace(Table table)
    {
        var row = table.AddRow();
        row.Height = 30;
        row.Borders.Visible = false;
        new Font { Color = ColorsHelper.RED_LIGHT };
    }
    
    private byte[] RenderDocument(MigraDoc.DocumentObjectModel.Document document)
    {
        var renderer = new PdfDocumentRenderer { Document = document, };
        
        renderer.RenderDocument();

        using var file = new MemoryStream();
        renderer.PdfDocument.Save(file);

        return file.ToArray();
    }
}