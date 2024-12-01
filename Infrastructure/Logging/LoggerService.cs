namespace Infrastructure.Logging;

using Application.Interfaces;
using Serilog;

public class LoggerService : ILoggerService
{
    public LoggerService()
    {
        string logDirectory = "logs";
        if (!Directory.Exists(logDirectory))
        {
            Directory.CreateDirectory(logDirectory);
        }

        Log.Logger = new LoggerConfiguration()
           .MinimumLevel.Information()
           .WriteTo.File(Path.Combine(logDirectory, "log.txt"), rollingInterval: RollingInterval.Day)
           .CreateLogger();
    }

    public void LogInformation(string message)
    {
        Log.Information(message);
    }

    public void LogWarning(string message)
    {
        Log.Warning(message);
    }

    public void LogError(string message, Exception exception)
    {
        Log.Error(exception, message);
    }
}
