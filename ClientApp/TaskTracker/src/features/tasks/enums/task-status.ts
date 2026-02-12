export const TaskStatus = {
  New: 0,
  InProgress: 1,
  Done: 2,
} as const;

export type TaskStatus =
  (typeof TaskStatus)[keyof typeof TaskStatus];
  
export const TaskStatusLabel: Record<TaskStatus, string> = {
  0: 'New',
  1: 'In Progress',
  2: 'Done',
};
