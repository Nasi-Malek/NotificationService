using Microsoft.AspNetCore.Mvc;
using NotificationContracts;
using ActivityService.Models;

namespace ActivityService.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ActivitiesController : ControllerBase
{
    private readonly Notifier.NotifierClient _client;
    private readonly ILogger<ActivitiesController> _logger;

    public ActivitiesController(Notifier.NotifierClient client, ILogger<ActivitiesController> logger)
    {
        _client = client;
        _logger = logger;
    }

    // POST /api/activities
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ActivityDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var activity = new Activity
        {
            UserId = dto.UserId,
            Action = dto.Action,
            Source = dto.Source ?? "",
            TimestampUnix = dto.TimestampUnix ?? DateTimeOffset.UtcNow.ToUnixTimeSeconds()
        };

        // call gRPC
        var reply = await _client.SubmitActivityAsync(activity, cancellationToken: ct);

        _logger.LogInformation("Sent activity to NotificationService. Reply: {Msg}", reply.Message);

        return Ok(new
        {
            sent = activity,
            notificationReply = reply.Message
        });
    }
}
