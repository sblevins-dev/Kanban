export interface Task {
    id: number;
    sprintId?: number;
    title: string;
    description: string;
    status: TaskStatus;
    assigneeId?: number | null;
}

export type TaskStatus = 'Backlog' | 'InProgress' | 'InReview' | 'Done';

export interface Column {
  name: TaskStatus;
  display: string;
  tasks: Task[];
}