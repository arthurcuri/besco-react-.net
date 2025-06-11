using BescoTaskApi.DTOs;
using BescoTaskApi.Models;

namespace BescoTaskApi.Services
{
    public interface ITaskService
    {
        Task<List<Models.Task>> GetAllTasksAsync();
        Task<List<Models.Task>> GetTasksByStatusAsync(BescoTaskApi.Models.TaskStatus status);
        Task<Models.Task?> GetTaskByIdAsync(long id);
        Task<Models.Task> CreateTaskAsync(TaskRequestDTO taskRequestDTO);
        Task<Models.Task?> UpdateTaskAsync(long id, TaskRequestDTO taskRequestDTO);
        Task<bool> DeleteTaskAsync(long id);
        Task<List<Models.Task>> GetPendingTasksAsync();
        Task<List<Models.Task>> GetInProgressTasksAsync();
        Task<List<Models.Task>> GetCompletedTasksAsync();
        Task<long> GetTotalTasksCountAsync();
        Task<long> GetTasksCountByStatusAsync(BescoTaskApi.Models.TaskStatus status);
    }
} 