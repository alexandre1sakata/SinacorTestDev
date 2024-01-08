using Microsoft.EntityFrameworkCore;
using SinacorTestDev.API.Apresentation.BackgroundServices;
using SinacorTestDev.API.Business.Services.Interfaces;
using SinacorTestDev.API.Infra.Data.Context;
using SinacorTestDev.API.Infra.Data.Repositories;
using SinacorTestDev.API.Infra.RabbitMQ.Interfaces;
using SinacorTestDev.API.Infra.RabbitMQ.Sevices;
using SinacorTestDev.API.Infra.RabbitMQ;
using SinacorTestDev.API.Services;
using Serilog;
using SinacorTestDev.API.Apresentation.Middleware;

var allowOrigins = "_allowOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowOrigins, policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<TaskContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TaskContext")));

// Add services to the container.
builder.Services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));
builder.Services.AddScoped<IUserTaskService, UserTaskService>();
builder.Services.AddScoped<IRabbitService, RabbitService>();
builder.Services.AddScoped<IRabbitMQProducer, RabbitMQProducer>();
builder.Services
    .AddSingleton<IRabbitMQConsumer, RabbitMQConsumer>()
    .AddHostedService<RabbitMQManagementService>();

// logs
Log.Logger = new LoggerConfiguration().CreateBootstrapLogger();
builder.Host.UseSerilog(((ctx, lc) => lc
.ReadFrom.Configuration(ctx.Configuration)));

// Add services to the container.

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

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseSerilogRequestLogging();

app.UseCors(allowOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
