using SinacorTestDev.WebAPI.Models;

namespace SinacorTestDev.WebAPI.Services.Interface;

public interface IUserTaskService
{
    IEnumerable<UserTask>? GetAll();
    UserTask? GetByName(string taskName);
    void Add(UserTask entity);
    void Modify(UserTask entity);
    void ChangeTaskStatus(int taskId, string newStatus);
    void Remove(int id);
}
