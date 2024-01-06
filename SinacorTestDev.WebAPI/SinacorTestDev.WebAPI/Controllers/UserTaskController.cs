using Microsoft.AspNetCore.Mvc;
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
        try
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = _userTaskService.GetAll();
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{taskName}")]
    public ActionResult<List<UserTask>> GetTaskByName(string taskName)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = _userTaskService.GetByName(taskName);

            if (result?.Any() != null)
            {
                string message = "Task not found.";
                _logger.LogError(message);
                return NotFound(message);
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return LogError(ex);
        }
    }

    [HttpPost]
    public ActionResult AddTask(UserTask userTask)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _userTaskService.Add(userTask);

            return Ok();
        }
        catch (Exception ex)
        {
            return LogError(ex);
        }
    }

    [HttpPut("{id}")]
    public ActionResult ModifyUserTask(UserTask userTask)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _userTaskService.Modify(userTask);
            return Ok();
        }
        catch (Exception ex)
        {
            return LogError(ex);
        }
        
    }

    [HttpPut("ChangeStatus/{id}/{newStatus}")]
    public ActionResult ChangeTaskStatus(int id, string newStatus)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _userTaskService.ChangeTaskStatusInQueue(id, newStatus);
            return Ok();
        }
        catch (Exception ex)
        {
            return LogError(ex);
        }
    }

    [HttpDelete("{id}")]
    public ActionResult RemoveUserTask(int id)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _userTaskService.Remove(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return LogError(ex);
        }
    }

    #region Private Methods

    private ActionResult LogError(Exception ex)
    {
        _logger.LogError(ex.Message);
        return StatusCode(500, ex.Message);
    }

    #endregion
}
