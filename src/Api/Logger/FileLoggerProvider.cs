namespace Exadel.Forecast.Api.Logger
{
    public class FileLoggerProvider : ILoggerProvider
    {
        private readonly string _pathFile;
        private FileLogger? _fileLogger;

        public FileLoggerProvider(string pathFile)
        {
            _pathFile = pathFile;
        }

        public ILogger CreateLogger(string categoryName)
        {
            _fileLogger = new FileLogger(_pathFile);
            return _fileLogger;
        }

        public void Dispose()
        {
            _fileLogger?.Dispose();
        }
    }
}
