using DataAccess.Contexts;
using Entities.Concretes;

public class ActivityLoggerService
{
    private readonly LogDBContext _context;

    public ActivityLoggerService(LogDBContext context)
    {
        _context = context;
    }

    public async Task LogActivity(int userId, string activityType, string description)
    {
        var activityLog = new Activity
        {
            UserId = userId,
            ActivityType = activityType,
            Date = DateTime.UtcNow,
            Description = description
        };

        _context.Activities.Add(activityLog);
        await _context.SaveChangesAsync();
    }
}