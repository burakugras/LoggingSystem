using Business.Abstracts;
using Business.Dtos.Requests.ActivityRequests;
using Core.DataAccess.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class ActivitiesController : Controller
    {
        IActivityService _activityService;

        public ActivitiesController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CreateActivityRequest createActivityRequest)
        {
            var result = await _activityService.AddAsync(createActivityRequest);
            return Ok(result);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] PageRequest pageRequest)
        {
            var result = await _activityService.GetAllAsync(pageRequest);
            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateActivityRequest updateActivityRequest)
        {
            var result = await _activityService.UpdateAsync(updateActivityRequest);
            return Ok(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            var result = await _activityService.DeleteAsync(id);
            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            var result = await _activityService.GetById(id);
            return Ok(result);
        }
    }
}
