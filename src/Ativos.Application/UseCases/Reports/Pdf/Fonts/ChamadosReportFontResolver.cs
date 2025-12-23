using System.Reflection;
using MigraDoc.DocumentObjectModel;
using PdfSharp.Fonts;

namespace Ativos.Application.UseCases.Reports.Pdf.Fonts;

public class ChamadosReportFontResolver : IFontResolver //criacao de fontes para utilizar no projeto
{
    public FontResolverInfo? ResolveTypeface(string familyName, bool bold, bool italic)
    {
        return new FontResolverInfo(familyName);
    }

    public byte[]? GetFont(string faceName)
    {
        var stream = ReadFontFile(faceName);

        if (stream is null) stream = ReadFontFile(FontHelper.DEFAULT_FONT);
        
        var length = (int)stream!.Length; //nao vai ser nulo
        
        var data = new byte[length];
        
        stream.Read(data, offset: 0, count: length);

        return data;
    }

    private Stream? ReadFontFile(string faceName)
    {
        var assembly = Assembly.GetExecutingAssembly();

        return assembly.GetManifestResourceStream($"Ativos.Application.UsCases.Reports.Pdf.Fonts.{faceName}.ttf");
    }
}