using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using System.Runtime.Versioning;

namespace APICatalogo.Logging;
[UnsupportedOSPlatform("browser")]
public class CustomLoggerProvider : ILoggerProvider
{
    readonly CustomLoggerProviderConfiguration loggerConfiguration;
    readonly ConcurrentDictionary<string, CustomLogger> loggers =
        new ConcurrentDictionary<string, CustomLogger>();

    public CustomLoggerProvider(IOptionsMonitor<CustomLoggerProviderConfiguration> configuration)
    {
        loggerConfiguration = configuration.CurrentValue;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return loggers.GetOrAdd(categoryName, name => new CustomLogger(name, loggerConfiguration));
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
