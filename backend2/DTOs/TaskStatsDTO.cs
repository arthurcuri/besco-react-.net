namespace BescoTaskApi.DTOs
{
    public class TaskStatsDTO
    {
        public long Total { get; set; }
        public long Pending { get; set; }
        public long InProgress { get; set; }
        public long Completed { get; set; }
        
        public TaskStatsDTO(long total, long pending, long inProgress, long completed)
        {
            Total = total;
            Pending = pending;
            InProgress = inProgress;
            Completed = completed;
        }
    }
} 