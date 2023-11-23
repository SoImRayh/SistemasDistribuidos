namespace APICatalogo.Logging
{
    public class CustomLogger : ILogger
    {
        readonly string loggerName;
        readonly CustomLoggerProviderConfiguration customLoggerProviderConfig;

        public CustomLogger(string loggerName, CustomLoggerProviderConfiguration customLoggerProviderConfiguration)
        {
            this.loggerName = loggerName;
            this.customLoggerProviderConfig = customLoggerProviderConfiguration;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == customLoggerProviderConfig.LogLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            string message = $"{logLevel}: {eventId} - {formatter(state,exception)}";
            //Configura nossa string de erro e escreve ela em um arquivo txt  de logs
            EscreverTextoNoArquivo(message);
        }

        private void EscreverTextoNoArquivo(string message)
        {
            var caminhoDoArquivo = $@"{Directory.GetCurrentDirectory()}\..\log.txt";
            using StreamWriter streamWriter = new StreamWriter(caminhoDoArquivo,true);
            try
            {
                streamWriter.WriteLine(message);
                streamWriter.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
