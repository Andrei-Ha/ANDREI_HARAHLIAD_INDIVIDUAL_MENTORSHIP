namespace Exadel.Forecast.Api.Logger
{
    public class FileLoggerProvider : ILoggerProvider
    {
        private readonly string _pathFile;

        public FileLoggerProvider(string pathFile)
        {
            _pathFile = pathFile;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(_pathFile);
        }

        public void Dispose() { }
    }
}
