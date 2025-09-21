using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = "Backlog";
        public DateTime CreatedAt { get; set; }
        public int SprintId { get; set; }
        public AssigneeDto? Assignee { get; set; }
    }
}
