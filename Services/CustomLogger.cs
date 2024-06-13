namespace WebApiSample.Services
{
    /// <summary>
    /// This code is intended to demo the injection of multiple services implmenting the same interface
    /// </summary>
    
    public interface ICustomLogger
    { 
        void Log (string message);
    }

    public class SimpleLogger : ICustomLogger
    {
        private readonly ILogger _logger;

        public SimpleLogger(ILogger<string> logger) 
        { 
            _logger = logger;
        }

        public void Log(string message)
        {
            _logger.LogInformation($"\nLogged by {nameof(SimpleLogger)}: {message}\n");
        }
    }

    public class DetailedLogger : ICustomLogger
    {
        private readonly ILogger _logger;

        public DetailedLogger(ILogger<string> logger)
        {
            _logger = logger;
        }

        public void Log(string message)
        {
            _logger.LogInformation($"\nLogged by {nameof(DetailedLogger)} at {DateTime.Now.ToString("HH:mm:ss dd-MMM-yyyy")}: {message}\n");
        }
    }
}
