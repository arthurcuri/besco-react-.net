using System.ComponentModel;

namespace BescoTaskApi.Models
{
    public enum TaskStatus
    {
        [Description("Tarefas")]
        Pending,
        
        [Description("Em andamento")]
        InProgress,
        
        [Description("Concluídas")]
        Completed
    }
} 