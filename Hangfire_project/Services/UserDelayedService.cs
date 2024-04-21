using Hangfire_project.Interfaces;

namespace Hangfire_project.Services;

public class UserDelayedService : IUserDelayedService
{
    private readonly ILogger<UserDelayedService> _logger;
    
    public UserDelayedService(ILogger<UserDelayedService> logger)
    {
        _logger = logger;
    }
    
    public void FireAndForgetJob()
    {
        _logger.LogInformation("Hello from a Fire and Forget job!");
    }
    public void ReccuringJob()
    {
        _logger.LogInformation("Hello from a Scheduled job!");
    }
    public void DelayedJob()
    {
        _logger.LogInformation("Hello from a Delayed job!");
    }
    public void ContinuationJob()
    {
        _logger.LogInformation("Hello from a Continuation job!");
    }
}