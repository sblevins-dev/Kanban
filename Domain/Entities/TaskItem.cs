using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Enums.TaskStatus Status { get; set; } = Enums.TaskStatus.Backlog;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int SprintId { get; set; }
        public Sprint? Sprint { get; set; }
        public int? AssigneeId { get; set; }
        public User? Assignee { get; set; }
    }
}
