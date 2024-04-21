using Hangfire;
using Hangfire_project.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hangfire_project.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserDelayedService _userDelayedService;
    private readonly IBackgroundJobClient _jobClient;
    private readonly IRecurringJobManager _recurringJobManager;
    
    public UserController(ILogger<UserController> logger, IUserDelayedService userDelayedService, IBackgroundJobClient jobClient, IRecurringJobManager recurringJobManager)
    {
        _logger = logger;
        _userDelayedService = userDelayedService;
        _jobClient = jobClient;
        _recurringJobManager = recurringJobManager;
    }
    
    [HttpPost("FireAndForget")]
    [ProducesResponseType(200)]
    public async Task<ActionResult> AddUserFireAndForget()
    {
        _logger.LogInformation("Add user fire and forget");

        _jobClient.Enqueue(() => _userDelayedService.FireAndForgetJob());
        
        return Ok();
    }
    
    [HttpPost("Schedule")]
    [ProducesResponseType(200)]
    public async Task<ActionResult> AddUserDelayed()
    {
        _logger.LogInformation("Add user delayed");

        var jobId= _jobClient.Schedule(() => _userDelayedService.DelayedJob(), TimeSpan.FromSeconds(10));
        
        return Ok(jobId);
    }
    
    [HttpPost("Recurring")]
    [ProducesResponseType(200)]
    public async Task<ActionResult> AddUserRecurring(string title)
    {
        _logger.LogInformation("Add user Recurring");

        _recurringJobManager.AddOrUpdate(title, () => _userDelayedService.ReccuringJob(), Cron.Minutely);
        
        return Ok();
    }
    
    [HttpPost("Continuations")]
    [ProducesResponseType(200)]
    public async Task<ActionResult> AddUserContinuations(string continuationId)
    {
        _logger.LogInformation("Add user Recurring");

        _jobClient.ContinueJobWith(continuationId, () => _userDelayedService.ContinuationJob());
        
        return Ok();
    }
}