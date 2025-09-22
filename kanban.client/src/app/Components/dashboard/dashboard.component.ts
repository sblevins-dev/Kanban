import { Component, OnInit } from '@angular/core';
import { Task, Column, TaskStatus } from './models';
import { TaskService } from '../../Services/task.service';



@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
})
export class DashboardComponent implements OnInit {
  columns: Column[] = [
    { name: 'Backlog', display: 'Backlog', tasks: [] },
    { name: 'InProgress', display: 'In Progress', tasks: [] },
    { name: 'InReview', display: 'In Review', tasks: [] },
    { name: 'Done', display: 'Done', tasks: [] },
  ];

  draggedTask: Task | null = null;
  draggedFromColumn: TaskStatus | null = null;

  constructor(private taskService: TaskService) { }

  ngOnInit(): void {
    this.loadTasks();
  }

  loadTasks(): void {
    this.taskService.getAllTasks().subscribe(tasks => {
      this.columns.forEach(column => {
        column.tasks = tasks.filter(task => task.status === column.name);
      });
    });
  }

  displayName(taskStatus: TaskStatus): string {
    switch (taskStatus) {
      case 'InProgress': return 'In Progress';
      case 'InReview': return 'In Review';
      default: return taskStatus;
    }
  }


  onDragStart(event: DragEvent, task: Task, fromColumn: TaskStatus): void {
    this.draggedTask = task;
    this.draggedFromColumn = fromColumn;
  }

  onDragOver(event: DragEvent): void {
    event.preventDefault(); // allow drop
  }

  onDrop(event: DragEvent, toColumn: TaskStatus): void {
    if (!this.draggedTask || !this.draggedFromColumn) return;

    // update status locally
    this.draggedTask.status = toColumn;

    const fromColumnObj = this.columns.find(c => c.name === this.draggedFromColumn)!;
    const toColumnObj = this.columns.find(c => c.name === toColumn)!;

    fromColumnObj.tasks = fromColumnObj.tasks.filter(t => t.id !== this.draggedTask!.id);
    toColumnObj.tasks.push(this.draggedTask);

    // create DTO for backend
    const taskUpdateDto = {
      status: this.draggedTask.status,
      assigneeId: this.draggedTask.assigneeId ?? null,
      title: this.draggedTask.title,
      description: this.draggedTask.description,
      sprintId: this.draggedTask.sprintId
    };

    // persist to backend
    this.taskService.updateTask(this.draggedTask.id, taskUpdateDto).subscribe();

    this.draggedTask = null;
    this.draggedFromColumn = null;
  }


}
