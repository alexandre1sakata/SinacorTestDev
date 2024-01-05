using Microsoft.EntityFrameworkCore;
using SinacorTestDev.WebAPI.Infra.Data.Context;
using SinacorTestDev.WebAPI.Infra.Data.Repository.Interfaces;
using SinacorTestDev.WebAPI.Models;
using System.Reflection;

namespace SinacorTestDev.WebAPI.Infra.Data.Repository;

public class RepositoryBase<T> : IRepository<T> where T : class
{
    private readonly TaskContext _taskContext;
    private readonly DbSet<T> _dbSet;

    public RepositoryBase(TaskContext taskContext) 
    {
        _taskContext = taskContext;
        _dbSet = _taskContext.Set<T>();
    }

    public IEnumerable<T>? SelectAll() 
        => _dbSet.ToList();

    public IEnumerable<T>? SelectByName(string name)
     => _dbSet.Where(entity => (entity as UserTask).Name.Contains(name));

    public T? SelectById(int id)
        => _dbSet.Find(id);

    public void Insert(T entity)
    {
        _dbSet.Add(entity);
        _taskContext.SaveChanges();
    }

    public void Update(T entity)
    {
        _taskContext.Entry(entity).State = EntityState.Modified;
        _taskContext.SaveChanges();
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
        _taskContext.SaveChanges();
    }
}
