using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task AddTaskAsync(TaskCreateDto dto)
        {
            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = Domain.Enums.TaskStatus.Backlog,
                SprintId = dto.SprintId,
                AssigneeId = dto.AssigneeId,
                CreatedAt = DateTime.UtcNow,
            };

            await _taskRepository.AddAsync(task);
        }

        public Task DeleteTaskAsync(int id)
        {
            return _taskRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<TaskDto>> GetAllTasksAsync()
        {
            var tasks = await _taskRepository.GetAllAsync();
            return tasks.Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status.ToString(),
                CreatedAt = t.CreatedAt,
                SprintId = t.SprintId,
                Assignee = t.Assignee != null ? new AssigneeDto
                {
                    Id = t.Assignee.Id,
                    Username = t.Assignee.Username
                } : null
            });
        }

        public async Task<TaskDto?> GetTaskByIdAsync(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null) return null;
            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status.ToString(),
                CreatedAt = task.CreatedAt,
                SprintId = task.SprintId,
                Assignee = task.Assignee != null ? new AssigneeDto
                {
                    Id = task.Assignee.Id,
                    Username = task.Assignee.Username
                } : null
            };
        }

        public async Task UpdateTaskAsync(int taskId, TaskUpdateDto dto)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null) throw new Exception("Task not found");

            if (!string.IsNullOrEmpty(dto.Title)) task.Title = dto.Title;
            if (!string.IsNullOrEmpty(dto.Description)) task.Description = dto.Description;
            if (dto.Status.HasValue) task.Status = dto.Status.Value;

            if (dto.SprintId.HasValue) task.SprintId = dto.SprintId.Value;
            if (dto.AssigneeId.HasValue) task.AssigneeId = dto.AssigneeId.Value;
            else if (dto.AssigneeId == null) task.AssigneeId = null;

            await _taskRepository.UpdateAsync(task);
        }
    }
}
