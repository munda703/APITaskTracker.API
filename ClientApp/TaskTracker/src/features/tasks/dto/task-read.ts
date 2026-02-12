import { TaskStatus } from '../enums/task-status';
import { TaskPriority } from '../enums/task-priority';

export interface TaskReadDto {
  id: number;
  title: string;
  description?: string;
  status: TaskStatus;
  priority: TaskPriority;
  dueDate?: string;
  createdAt: string;
}