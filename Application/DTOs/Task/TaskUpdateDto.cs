using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Task
{
    public class TaskUpdateDto
    {
        public Domain.Enums.TaskStatus? Status { get; set; }
        public int? AssigneeId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? SprintId { get; set; }
    }
}
