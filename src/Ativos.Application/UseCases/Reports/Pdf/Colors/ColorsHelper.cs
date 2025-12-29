using System.Drawing;
using Color = MigraDoc.DocumentObjectModel.Color;

namespace Ativos.Application.UseCases.Reports.Pdf.Colors;

public static class ColorsHelper
{
    public static readonly MigraDoc.DocumentObjectModel.Color RED_DARK = Color.Parse("#DF340C");
    public static readonly MigraDoc.DocumentObjectModel.Color RED_LIGHT = Color.Parse("#F5C2B6");
    public static readonly MigraDoc.DocumentObjectModel.Color GREEN_DARK = Color.Parse("#D7E5E5");
    public static readonly MigraDoc.DocumentObjectModel.Color GREEN_LIGHT = Color.Parse("#F5F9F8");
    public static readonly MigraDoc.DocumentObjectModel.Color BLACK = Color.Parse("#000000");
    public static readonly MigraDoc.DocumentObjectModel.Color WHITE = Color.Parse("#FFFFFF");
}