﻿namespace SinacorTestDev.API.Business.Services.Interfaces;

public interface IRepository<T>
{
    IEnumerable<T>? SelectAll();
    IEnumerable<T> SelectByName(string name);
    T? SelectById(int id);
    void Insert(T entity);
    void Update(T entity);
    void Delete(T entity);
}
