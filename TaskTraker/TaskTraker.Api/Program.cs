using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TaskTraker.Api.Middlewares;
using TaskTraker.Api.Services;
using TaskTraker.Data.Context;
using TaskTraker.Data.Models;
using TaskTraker.Services.Interfaces;
using TaskTraker.Services.Services;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration["TaskTraker:ConnectionString"];

//CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("https://khajiit73.github.io")
              .AllowAnyHeader() 
              .AllowAnyMethod() 
              .SetIsOriginAllowed(origin => true) 
              .AllowCredentials(); 
    });
});

builder.Services.AddAuthorization();
builder.Services.AddControllers();

// Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

// Add other services
builder.Services.AddDbContext<TaskTrakerDbContext>(options =>
    options.UseNpgsql(connection)
);

builder.Services.AddScoped<IBoardService, BoardService>();
builder.Services.AddScoped<ITaskItemService, TaskItemService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

var app = builder.Build();

// Swagger UI setup
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskTraker API V1");
    c.RoutePrefix = "swagger";
});

// Middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Use CORS
app.UseCors("AllowSpecificOrigin");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
