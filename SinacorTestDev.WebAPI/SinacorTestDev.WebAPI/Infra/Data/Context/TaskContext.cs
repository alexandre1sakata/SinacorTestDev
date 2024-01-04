using Microsoft.EntityFrameworkCore;
using SinacorTestDev.WebAPI.Models;

namespace SinacorTestDev.WebAPI.Infra.Data.Context
{
    public class TaskContext : DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> options)
            :base(options)
        { }

        public DbSet<UserTask>? UserTasks { get; set; }
    }
}
