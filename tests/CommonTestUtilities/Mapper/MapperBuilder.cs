

using Ativos.Application.AutoMapper;
using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;

namespace CommonTestUtilities.Mapper;

public class MapperBuilder
{
    public static IMapper Build()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AutoMapping());
        }, NullLoggerFactory.Instance);

        return config.CreateMapper();
    }
}    