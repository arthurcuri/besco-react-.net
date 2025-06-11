using BescoTaskApi.Data;
using BescoTaskApi.DTOs;
using BescoTaskApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BescoTaskApi.Services
{
    public class TaskService : ITaskService
    {
        private readonly TaskDbContext _context;
        
        public TaskService(TaskDbContext context)
        {
            _context = context;
        }
        
        public async Task<List<Models.Task>> GetAllTasksAsync()
        {
            return await _context.Tasks
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }
        
        public async Task<List<Models.Task>> GetTasksByStatusAsync(BescoTaskApi.Models.TaskStatus status)
        {
            return await _context.Tasks
                .Where(t => t.Status == status)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }
        
        public async Task<Models.Task?> GetTaskByIdAsync(long id)
        {
            return await _context.Tasks.FindAsync(id);
        }
        
        public async Task<Models.Task> CreateTaskAsync(TaskRequestDTO taskRequestDTO)
        {
            if (string.IsNullOrWhiteSpace(taskRequestDTO.Title))
            {
                throw new ArgumentException("O título da tarefa é obrigatório");
            }
            
            var task = new Models.Task
            {
                Title = taskRequestDTO.Title.Trim(),
                Description = taskRequestDTO.Description,
                CreatedAt = DateTime.Now
            };
            
            // Convert string to enum
            if (!string.IsNullOrWhiteSpace(taskRequestDTO.Status))
            {
                if (Enum.TryParse<BescoTaskApi.Models.TaskStatus>(taskRequestDTO.Status.Trim(), true, out var status))
                {
                    task.Status = status;
                }
                else
                {
                    throw new ArgumentException($"Status inválido: {taskRequestDTO.Status}");
                }
            }
            else
            {
                task.Status = BescoTaskApi.Models.TaskStatus.Pending;
            }
            
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }
        
        public async Task<Models.Task?> UpdateTaskAsync(long id, TaskRequestDTO taskRequestDTO)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return null;
            }
            
            if (!string.IsNullOrWhiteSpace(taskRequestDTO.Title))
            {
                task.Title = taskRequestDTO.Title.Trim();
            }
            
            if (taskRequestDTO.Description != null)
            {
                task.Description = taskRequestDTO.Description;
            }
            
            if (!string.IsNullOrWhiteSpace(taskRequestDTO.Status))
            {
                if (Enum.TryParse<BescoTaskApi.Models.TaskStatus>(taskRequestDTO.Status.Trim(), true, out var status))
                {
                    task.Status = status;
                }
                else
                {
                    throw new ArgumentException($"Status inválido: {taskRequestDTO.Status}");
                }
            }
            
            await _context.SaveChangesAsync();
            return task;
        }
        
        public async Task<bool> DeleteTaskAsync(long id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return false;
            }
            
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
        
        public async Task<List<Models.Task>> GetPendingTasksAsync()
        {
            return await GetTasksByStatusAsync(BescoTaskApi.Models.TaskStatus.Pending);
        }
        
        public async Task<List<Models.Task>> GetInProgressTasksAsync()
        {
            return await GetTasksByStatusAsync(BescoTaskApi.Models.TaskStatus.InProgress);
        }
        
        public async Task<List<Models.Task>> GetCompletedTasksAsync()
        {
            return await GetTasksByStatusAsync(BescoTaskApi.Models.TaskStatus.Completed);
        }
        
        public async Task<long> GetTotalTasksCountAsync()
        {
            return await _context.Tasks.CountAsync();
        }
        
        public async Task<long> GetTasksCountByStatusAsync(BescoTaskApi.Models.TaskStatus status)
        {
            return await _context.Tasks.CountAsync(t => t.Status == status);
        }
    }
} 