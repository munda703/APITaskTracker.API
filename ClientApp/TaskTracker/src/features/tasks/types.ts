import type { TaskReadDto } from "./dto/task-read";

export interface TasksState {
  items: TaskReadDto[];
  loading: boolean;
  error: string | null;
}