using SinacorTestDev.WebAPI.Models;

namespace SinacorTestDev.WebAPI.Services.Interface;

public interface IUserTaskService
{
    IEnumerable<UserTask>? GetAll();
    UserTask? GetByName(string taskName);
    void Add(UserTask entity);
    void Modify(UserTask entity);
    void Remove(int id);
}
