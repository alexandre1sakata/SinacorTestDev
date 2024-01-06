﻿using SinacorTestDev.WebAPI.Infra.Data.Repository.Interfaces;
using SinacorTestDev.WebAPI.Models;
using SinacorTestDev.WebAPI.Services.Interface;

namespace SinacorTestDev.WebAPI.Services;

public class UserTaskService : IUserTaskService
{
    private readonly IRepository<UserTask> _userTaskRepository;
    private readonly IRabbitService _rabbitManagementService;

    public UserTaskService(
        IRepository<UserTask> userTaskRepository, 
        IRabbitService rabbitManagementService)
    {
        _userTaskRepository = userTaskRepository;
        _rabbitManagementService = rabbitManagementService;
    }

    public IEnumerable<UserTask>? GetAll() 
        => _userTaskRepository.SelectAll();

    public IEnumerable<UserTask>? GetByName(string taskName)
        => _userTaskRepository.SelectByName(taskName);

    public void Add(UserTask userTask)
    {
        userTask.SetCreatedDate();
        _userTaskRepository.Insert(userTask);
    }

    public void Modify(UserTask userTask)
    {
        userTask.SetLastModifiedDate();
        _userTaskRepository.Update(userTask);
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

        _rabbitManagementService.SendObjectMessage(userTask);
    }

    public void ChangeTaskStatus(UserTask userTask)
    {
        userTask.SetLastModifiedDate();
        _userTaskRepository.Update(userTask);
    }
}
