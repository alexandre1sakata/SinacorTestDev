using Microsoft.EntityFrameworkCore;
using SinacorTestDev.API.Business.Models;

namespace SinacorTestDev.API.Infra.Data.Context;

public class TaskContext : DbContext
{
    public TaskContext(DbContextOptions<TaskContext> options)
        :base(options)
    { }

    public DbSet<UserTask>? UserTasks { get; set; }
}
