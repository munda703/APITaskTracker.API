import type { TasksState } from "../types";

export const initialState: TasksState = {
  items: [],
  loading: false,
  error: null,
};