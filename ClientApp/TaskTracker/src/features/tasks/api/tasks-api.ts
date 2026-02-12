import api from '@/lib/axios';
import type { TaskReadDto } from '../dto/task-read';
import type { TaskCreateDto } from '../dto/task-create';
import type { TaskUpdateDto } from '../dto/task-update';

export const getTasks = async (
  q?: string,
  sort: 'asc' | 'desc' = 'asc'
): Promise<TaskReadDto[]> => {
  const res = await api.get('/tasks', {
    params: {
      q,
      sort, 
    },
  });

  return res.data;
};

export const createTask = async (dto: TaskCreateDto) => {
  const res = await api.post('/tasks', dto);
  return res.data;
};

export const updateTask = async (id: number, dto: TaskUpdateDto) => {
  const res = await api.put(`/tasks/${id}`, dto);
  return res.data;
};

export const deleteTask = async (id: number) => {
  await api.delete(`/tasks/${id}`);
};

