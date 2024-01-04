using Microsoft.EntityFrameworkCore;
using SinacorTestDev.WebAPI.Infra.Data.Context;
using SinacorTestDev.WebAPI.Infra.Data.Repository;
using SinacorTestDev.WebAPI.Infra.Data.Repository.Interfaces;
using SinacorTestDev.WebAPI.Services;
using SinacorTestDev.WebAPI.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TaskContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("TaskContext")));

// Add services to the container.
builder.Services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));
builder.Services.AddScoped<IUserTaskService, UserTaskService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();