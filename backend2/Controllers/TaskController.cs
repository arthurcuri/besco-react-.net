using BescoTaskApi.DTOs;
using BescoTaskApi.Models;
using BescoTaskApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BescoTaskApi.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        
        // GET - Listar todas as tarefas
        [HttpGet]
        public async Task<ActionResult<List<Models.Task>>> GetAllTasks()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }
        
        // GET - Listar tarefas por status
        [HttpGet("status/{status}")]
        public async Task<ActionResult<List<Models.Task>>> GetTasksByStatus(BescoTaskApi.Models.TaskStatus status)
        {
            var tasks = await _taskService.GetTasksByStatusAsync(status);
            return Ok(tasks);
        }
        
        // GET - Buscar tarefa por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Task>> GetTaskById(long id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }
        
        // POST - Criar nova tarefa
        [HttpPost]
        public async Task<ActionResult<Models.Task>> CreateTask([FromBody] TaskRequestDTO taskRequestDTO)
        {
            try
            {
                var savedTask = await _taskService.CreateTaskAsync(taskRequestDTO);
                return CreatedAtAction(nameof(GetTaskById), new { id = savedTask.Id }, savedTask);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        // PUT - Atualizar tarefa
        [HttpPut("{id}")]
        public async Task<ActionResult<Models.Task>> UpdateTask(long id, [FromBody] TaskRequestDTO taskRequestDTO)
        {
            try
            {
                var updatedTask = await _taskService.UpdateTaskAsync(id, taskRequestDTO);
                
                if (updatedTask == null)
                {
                    return NotFound();
                }
                
                return Ok(updatedTask);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem($"Erro interno do servidor: {ex.Message}");
            }
        }
        
        // DELETE - Remover tarefa
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(long id)
        {
            try
            {
                bool deleted = await _taskService.DeleteTaskAsync(id);
                if (deleted)
                {
                    return NoContent();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return Problem($"Erro ao deletar tarefa: {ex.Message}");
            }
        }
        
        // GET - Listar apenas tarefas pendentes
        [HttpGet("pending")]
        public async Task<ActionResult<List<Models.Task>>> GetPendingTasks()
        {
            var tasks = await _taskService.GetPendingTasksAsync();
            return Ok(tasks);
        }
        
        // GET - Listar apenas tarefas em andamento
        [HttpGet("in-progress")]
        public async Task<ActionResult<List<Models.Task>>> GetInProgressTasks()
        {
            var tasks = await _taskService.GetInProgressTasksAsync();
            return Ok(tasks);
        }
        
        // GET - Listar apenas tarefas concluídas
        [HttpGet("completed")]
        public async Task<ActionResult<List<Models.Task>>> GetCompletedTasks()
        {
            var tasks = await _taskService.GetCompletedTasksAsync();
            return Ok(tasks);
        }
        
        // GET - Estatísticas do backlog
        [HttpGet("stats")]
        public async Task<ActionResult<TaskStatsDTO>> GetTasksStats()
        {
            var stats = new TaskStatsDTO(
                await _taskService.GetTotalTasksCountAsync(),
                await _taskService.GetTasksCountByStatusAsync(BescoTaskApi.Models.TaskStatus.Pending),
                await _taskService.GetTasksCountByStatusAsync(BescoTaskApi.Models.TaskStatus.InProgress),
                await _taskService.GetTasksCountByStatusAsync(BescoTaskApi.Models.TaskStatus.Completed)
            );
            return Ok(stats);
        }
    }
} 