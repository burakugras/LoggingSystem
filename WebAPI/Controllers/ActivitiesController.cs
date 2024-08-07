﻿using Business.Abstracts;
using Business.Dtos.Requests.ActivityRequests;
using Core.CrossCutingConcerns.Logging.SeriLog.Logger;
using Core.CrossCutingConcerns.Logging;
using Core.DataAccess.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ActivitiesController : ControllerBase
    {
        IActivityService _activityService;

        public ActivitiesController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        [Logging(typeof(MsSqlLogger))]
        [Logging(typeof(FileLogger))]
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CreateActivityRequest createActivityRequest)
        {
            var result = await _activityService.AddAsync(createActivityRequest);
            return Ok(result);
        }

        [Logging(typeof(MsSqlLogger))]
        [Logging(typeof(FileLogger))]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] PageRequest pageRequest)
        {
            var result = await _activityService.GetAllAsync(pageRequest);
            return Ok(result);
        }

        [Logging(typeof(MsSqlLogger))]
        [Logging(typeof(FileLogger))]
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateActivityRequest updateActivityRequest)
        {
            var result = await _activityService.UpdateAsync(updateActivityRequest);
            return Ok(result);
        }

        [Logging(typeof(MsSqlLogger))]
        [Logging(typeof(FileLogger))]
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            var result = await _activityService.DeleteAsync(id);
            return Ok(result);
        }

        [Logging(typeof(MsSqlLogger))]
        [Logging(typeof(FileLogger))]
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            var result = await _activityService.GetById(id);
            return Ok(result);
        }
    }
}
