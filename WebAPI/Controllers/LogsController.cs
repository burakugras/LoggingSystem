using DataAccess.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        return Ok(logs);
    }

    [HttpGet("GetByUserId/{userId}")]
    public async Task<IActionResult> GetByUserId(string userId)
    {
        var logs = await _context.Logs.Where(log => log.UserId == userId).ToListAsync();
        return Ok(logs);
    }

    [HttpGet("GetByDate")]
    public async Task<IActionResult> GetByDate(DateTimeOffset startDate, DateTimeOffset endDate)
    {
        var logs = await _context.Logs
            .Where(log => log.Date >= startDate && log.Date <= endDate)
            .ToListAsync();
        return Ok(logs);
    }
}
