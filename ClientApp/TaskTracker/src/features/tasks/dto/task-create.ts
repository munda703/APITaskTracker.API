import { TaskPriority } from '../enums/task-priority';

export interface TaskCreateDto {
  title: string;
  description?: string;
  priority: TaskPriority;
  dueDate?: string;
}
