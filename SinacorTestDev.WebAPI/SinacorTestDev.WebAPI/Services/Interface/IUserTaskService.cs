using SinacorTestDev.WebAPI.Models;

namespace SinacorTestDev.WebAPI.Services.Interface;

public interface IUserTaskService
{
    IEnumerable<UserTask>? GetAll();
    IEnumerable<UserTask> GetTasksByName(string taskName);
    UserTask? GetTaskById(int id);
    void Add(UserTask entity);
    void Modify(UserTask entity);
    void ChangeTaskStatusInQueue(int taskId, string newStatus);
    void ChangeTaskStatus(UserTask userTask);
    void Remove(int id);
}
