using Core.CrossCutingConcerns.Logging.DecriptionTools;
using DataAccess.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class LogsController : ControllerBase
{
    private readonly LogDBContext _context;

    public LogsController(LogDBContext context)
    {
        _context = context;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var logs = await _context.Logs.ToListAsync();

        var formattedLogs = logs.Select(log => new
        {
            log.Id,
            log.Date,
            Description = FormatDescription(log.Description),
            log.Username,
            log.UserId,
            log.ActivityType
        }).ToList();

        return Ok(formattedLogs);
    }

    [HttpGet("GetByUserId/{userId}")]
    public async Task<IActionResult> GetByUserId(string userId)
    {
        var logs = await _context.Logs.Where(log => log.UserId == userId).ToListAsync();

        var formattedLogs = logs.Select(log => new
        {
            log.Id,
            log.Date,
            Description = FormatDescription(log.Description),
            log.Username,
            log.UserId,
            log.ActivityType
        }).ToList();

        return Ok(formattedLogs);
    }

    [HttpGet("GetByDate")]
    public async Task<IActionResult> GetByDate(DateTimeOffset startDate, DateTimeOffset endDate)
    {
        var logs = await _context.Logs
            .Where(log => log.Date >= startDate && log.Date <= endDate)
            .ToListAsync();

        var formattedLogs = logs.Select(log => new
        {
            log.Id,
            log.Date,
            Description = FormatDescription(log.Description),
            log.Username,
            log.UserId,
            log.ActivityType
        }).ToList();

        return Ok(formattedLogs);
    }

    
    private string FormatDescription(string description)
    {
        try
        {
            var activityDescription = JsonConvert.DeserializeObject<ActivityDescription>(description);

            if (activityDescription == null)
            {
                return description; 
            }

            return $"ActivityType: {activityDescription.ActivityType}, " +
                   $"Username: {activityDescription.Username}, " +
                   $"UserId: {activityDescription.UserId}, " +
                   $"Parameters: {FormatParameters(activityDescription.Parameters)}";
        }
        catch (Exception)
        {
            return description;
        }
    }

    
    private string FormatParameters(List<Parameter> parameters)
    {
        if (parameters == null || parameters.Count == 0)
        {
            return "No parameters";
        }

        return string.Join(", ", parameters.Select(p => $"{p.Name}: {string.Join(",", p.Value)} ({p.Type})"));
    }
}


