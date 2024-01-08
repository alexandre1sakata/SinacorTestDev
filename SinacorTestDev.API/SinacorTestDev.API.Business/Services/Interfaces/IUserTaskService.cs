using SinacorTestDev.API.Business.Models;

namespace SinacorTestDev.API.Business.Services.Interfaces;

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
