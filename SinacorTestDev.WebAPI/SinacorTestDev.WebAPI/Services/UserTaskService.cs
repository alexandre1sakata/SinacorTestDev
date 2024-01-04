using SinacorTestDev.WebAPI.Infra.Data.Repository.Interfaces;
using SinacorTestDev.WebAPI.Models;
using SinacorTestDev.WebAPI.Services.Interface;

namespace SinacorTestDev.WebAPI.Services;

public class UserTaskService : IUserTaskService
{
    private readonly IRepository<UserTask> _userTaskRepository;

    public UserTaskService(IRepository<UserTask> userTaskRepository)
    {
        _userTaskRepository = userTaskRepository;
    }

    public IEnumerable<UserTask>? GetAll() 
        => _userTaskRepository.SelectAll();

    public UserTask? GetByName(string taskName) 
        => _userTaskRepository.SelectByName(taskName);

    public void Add(UserTask userTask) 
        => _userTaskRepository.Insert(userTask);

    public void Modify(UserTask entity) 
        => _userTaskRepository.Update(entity);

    public void Remove(int id)
    {
        var userTask = _userTaskRepository.SelectById(id);
        _userTaskRepository.Delete(userTask);
    }
}
