using Microsoft.EntityFrameworkCore;
using SinacorTestDev.API.Infra.Data.Context;
using SinacorTestDev.API.Business.Models;
using SinacorTestDev.API.Business.Services.Interfaces;

namespace SinacorTestDev.API.Infra.Data.Repositories;

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

    public IEnumerable<T> SelectByName(string name)
    {
        return _dbSet
            .AsEnumerable()
            .Where(entity => (entity as UserTask).GetName()
                .Contains(name, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

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
