namespace Hangfire_project.Interfaces;

public interface IUserDelayedService
{
    void FireAndForgetJob();
    void ReccuringJob();
    void DelayedJob();
    void ContinuationJob();
}