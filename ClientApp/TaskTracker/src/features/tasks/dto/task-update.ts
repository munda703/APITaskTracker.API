import { TaskPriority } from '../enums/task-priority';
import { TaskStatus } from '../enums/task-status';

export interface TaskUpdateDto {
  title?: string;
  description?: string;
  status?: TaskStatus;
  priority?: TaskPriority;
  dueDate?: string;
}