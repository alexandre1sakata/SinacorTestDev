﻿using Microsoft.AspNetCore.Mvc;
using SinacorTestDev.WebAPI.Models;
using SinacorTestDev.WebAPI.Services.Interface;

namespace SinacorTestDev.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserTaskController : ControllerBase
{
    private readonly IUserTaskService _userTaskService;
    private readonly ILogger<UserTaskController> _logger;

    public UserTaskController(IUserTaskService userTaskService, ILogger<UserTaskController> logger)
    {
        _userTaskService = userTaskService;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<List<UserTask>> GetAllTasks()
    {
        var result = _userTaskService.GetAll();
        return Ok(result);
    }

    [HttpGet("{taskName}")]
    public ActionResult<List<UserTask>> GetTaskByName(string taskName)
    {
        var result = _userTaskService.GetByName(taskName);

        if (!result.Any()) return NotFound("Task not found.");

        return Ok(result);
    }

    [HttpPost]
    public ActionResult AddTask(UserTask userTask)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        _userTaskService.Add(userTask);
        return Ok();
    }

    [HttpPut("{id}")]
    public ActionResult ModifyUserTask(UserTask userTask)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        _userTaskService.Modify(userTask);
        return Ok();

    }

    [HttpPut("ChangeStatus/{id}/{newStatus}")]
    public ActionResult ChangeTaskStatus(int id, string newStatus)
    {
        _userTaskService.ChangeTaskStatusInQueue(id, newStatus);
        return Ok();
    }

    [HttpDelete("{id}")]
    public ActionResult RemoveUserTask(int id)
    {
        _userTaskService.Remove(id);
        return Ok();
    }

    #region Private Methods

    private ActionResult LogError(Exception ex)
    {
        _logger.LogError(ex.Message);
        return StatusCode(500, ex.Message);
    }

    #endregion
}
