export const TaskPriority = {
  Low: 0,
  Medium: 1,
  High: 2,
} as const

export type TaskPriority = typeof TaskPriority[keyof typeof TaskPriority]

export const TaskPriorityLabel: Record<TaskPriority, string> = {
  0: 'Low',
  1: 'Medium',
  2: 'High',
};
