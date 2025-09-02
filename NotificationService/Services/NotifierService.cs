using Grpc.Core;
using Microsoft.Extensions.Logging;
using NotificationContracts;

namespace NotificationService.Services;

public sealed class NotifierService : Notifier.NotifierBase
{
    private readonly ILogger<NotifierService> _logger;
    public NotifierService(ILogger<NotifierService> logger) => _logger = logger;

    public override Task<Ack> SubmitActivity(Activity request, ServerCallContext context)
    {
        _logger.LogInformation("Activity received: user={UserId}, action={Action}, source={Source}, ts={Ts}",
            request.UserId, request.Action, request.Source, request.TimestampUnix);

        return Task.FromResult(new Ack
        {
            Message = $"Activity from {request.UserId} logged at {DateTimeOffset.UtcNow:O}"
        });
    }
}
