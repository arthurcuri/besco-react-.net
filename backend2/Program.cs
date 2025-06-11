using BescoTaskApi.Data;
using BescoTaskApi.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Configurar enums para serem serializados como strings
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Entity Framework
builder.Services.AddDbContext<TaskDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add custom services
builder.Services.AddScoped<ITaskService, TaskService>();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseAuthorization();

app.MapControllers();

// Create database and seed data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TaskDbContext>();
    context.Database.EnsureCreated();
    
    // Seed data if database is empty
    if (!context.Tasks.Any())
    {
        context.Tasks.AddRange(new[]
        {
            new BescoTaskApi.Models.Task
            {
                Title = "Configurar ambiente de desenvolvimento",
                Description = "Instalar e configurar todas as ferramentas necessárias",
                Status = BescoTaskApi.Models.TaskStatus.Completed,
                CreatedAt = DateTime.Now
            },
            new BescoTaskApi.Models.Task
            {
                Title = "Criar API REST",
                Description = "Desenvolver endpoints para gerenciar tarefas",
                Status = BescoTaskApi.Models.TaskStatus.InProgress,
                CreatedAt = DateTime.Now
            },
            new BescoTaskApi.Models.Task
            {
                Title = "Implementar frontend",
                Description = "Criar interface de usuário para o backlog",
                Status = BescoTaskApi.Models.TaskStatus.Pending,
                CreatedAt = DateTime.Now
            },
            new BescoTaskApi.Models.Task
            {
                Title = "Escrever documentação",
                Description = "Documentar a API e como usar o sistema",
                Status = BescoTaskApi.Models.TaskStatus.Pending,
                CreatedAt = DateTime.Now
            },
            new BescoTaskApi.Models.Task
            {
                Title = "Testes unitários",
                Description = "Implementar testes para os endpoints",
                Status = BescoTaskApi.Models.TaskStatus.Pending,
                CreatedAt = DateTime.Now
            }
        });
        context.SaveChanges();
    }
}

app.Run(); 