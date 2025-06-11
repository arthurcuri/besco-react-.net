using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BescoTaskApi.Models
{
    [Table("tasks")]
    public class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        
        [Required]
        [MaxLength(255)]
        public string Title { get; set; } = string.Empty;
        
        [MaxLength(500)]
        public string? Description { get; set; }
        
        [Required]
        [Column("status")]
        public TaskStatus Status { get; set; } = TaskStatus.Pending;
        
        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
} 