using System.Globalization;

namespace Ativos.Api.Middleware;

public class CultureMiddleware
{
    private readonly RequestDelegate _next;

    public CultureMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var supportedLanguages = new[] { "en", "en-US", "pt-BR" };
        var requestedCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();
        var cultureInfo = new CultureInfo("en");

        // Se o header Accept-Language está presente e é suportado
        if (!string.IsNullOrWhiteSpace(requestedCulture))
        {
            // Limpa qualquer informação de qualidade (q=0.9, etc.)
            var cultureName = requestedCulture.Split(',')[0].Split(';')[0].Trim();
            
            if (supportedLanguages.Contains(cultureName))
            {
                cultureInfo = new CultureInfo(cultureName);
            }
        }

        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;

        await _next(context);
    }
}