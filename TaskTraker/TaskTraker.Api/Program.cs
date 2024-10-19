using Microsoft.EntityFrameworkCore;
using TaskApp.Data.Context;
using TaskApp.Services.Interfaces;
using TaskApp.Services.Services;
using TaskTraker.Services.Services;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration["TaskTraker:ConnectionString"];

// Add services to the container.

builder.Services.AddAuthorization();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TaskTrakerDbContext>(options =>
    options.UseMySQL(connection)
);

builder.Services.AddScoped<ITaskItemService, TaskItemService>();
builder.Services.AddScoped<IBoardService, BoardService>();
builder.Services.AddScoped<ITaskItemService, TaskItemService>();

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
