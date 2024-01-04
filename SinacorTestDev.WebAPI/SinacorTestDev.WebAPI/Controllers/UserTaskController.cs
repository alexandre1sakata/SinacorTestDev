using Microsoft.AspNetCore.Mvc;
using SinacorTestDev.WebAPI.Models;
using SinacorTestDev.WebAPI.Services.Interface;

namespace SinacorTestDev.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserTaskController : ControllerBase
{
    private readonly IUserTaskService _userTaskService;

    public UserTaskController(IUserTaskService userTaskService)
    {
        _userTaskService = userTaskService;
    }

    [HttpGet]
    public ActionResult<List<UserTask>> GetAllTasks()
    {
        return Ok(_userTaskService.GetAll());
    }

    [HttpGet("{taskName}")]
    public ActionResult<UserTask> GetSingleHero(string taskName)
    {
        var result = _userTaskService.GetByName(taskName);
        if (result is null)
            return NotFound("Task not found.");

        return Ok(result);
    }

    [HttpPost]
    public ActionResult<List<UserTask>> AddHero(UserTask userTask)
    {
        _userTaskService.Add(userTask);
        return Ok();
    }

    [HttpPut("{id}")]
    public ActionResult<List<UserTask>> ModifyUserTask(UserTask userTask)
    {
        _userTaskService.Modify(userTask);
        return Ok();
    }

    [HttpDelete("{id}")]
    public ActionResult<List<UserTask>> RemoveUserTask(int id)
    {
        _userTaskService.Remove(id);
        return Ok();
    }
}
