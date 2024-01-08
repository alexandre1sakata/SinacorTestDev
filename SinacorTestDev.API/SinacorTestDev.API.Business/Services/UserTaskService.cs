using SinacorTestDev.API.Business.Models;
using SinacorTestDev.API.Business.Services.Interfaces;

namespace SinacorTestDev.API.Services;

public class UserTaskService : IUserTaskService
{
    private readonly IRepository<UserTask> _userTaskRepository;
    private readonly IRabbitService _rabbitService;

    public UserTaskService(
        IRepository<UserTask> userTaskRepository, 
        IRabbitService rabbitManagementService)
    {
        _userTaskRepository = userTaskRepository;
        _rabbitService = rabbitManagementService;
    }

    public IEnumerable<UserTask>? GetAll() 
        => _userTaskRepository.SelectAll();

    public IEnumerable<UserTask> GetTasksByName(string taskName)
        => _userTaskRepository.SelectByName(taskName);

    public UserTask? GetTaskById(int id)
        => _userTaskRepository.SelectById(id);

    public void Add(UserTask userTask)
    {
        userTask.SetCreatedDate();
        _userTaskRepository.Insert(userTask);
    }

    public void Modify(UserTask userTask)
    {
        var userTaskDb = _userTaskRepository.SelectById(userTask.Id);

        userTaskDb.SetName(userTask.Name);
        userTaskDb.SetDescription(userTask.Description);
        userTaskDb.SetLastModifiedDate();

        _userTaskRepository.Update(userTaskDb);
    }

    public void Remove(int id)
    {
        var userTask = _userTaskRepository.SelectById(id);
        _userTaskRepository.Delete(userTask);
    }

    public void ChangeTaskStatusInQueue(int taskId, string newStatus)
    {
        var userTask = _userTaskRepository.SelectById(taskId);
        userTask.ChangeStatus(newStatus);

        _rabbitService.SendObjectMessage(userTask);
    }

    public void ChangeTaskStatus(UserTask userTask)
    {
        userTask.SetLastModifiedDate();
        _userTaskRepository.Update(userTask);
    }
}
