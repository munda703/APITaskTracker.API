import { createSlice } from '@reduxjs/toolkit';
import { initialState } from './initialState';
import { createTaskThunk, deleteTaskThunk, fetchTasks, updateTaskThunk } from '../thunks/tasks-thunk';

const tasksSlice = createSlice({
  name: 'tasks',
  initialState,
  reducers: {},
  extraReducers: builder => {
    builder
      .addCase(fetchTasks.pending, state => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchTasks.fulfilled, (state, action) => {
        state.items = action.payload;
        state.loading = false;
      })
      .addCase(fetchTasks.rejected, (state, action) => {
        state.loading = false;

        state.error =
          action.payload?.detail ??
          action.error.message ??
          'Failed to load tasks';
      })
      .addCase(createTaskThunk.fulfilled, (state, action) => {
        state.items.push(action.payload);
      })
      .addCase(updateTaskThunk.fulfilled, (state, action) => {
        const index = state.items.findIndex(
          t => t.id === action.payload.id
        );
        if (index !== -1) {
          state.items[index] = action.payload;
        }
      })
      .addCase(deleteTaskThunk.fulfilled, (state, action) => {
        state.items = state.items.filter(
          t => t.id !== action.payload
        );
      });
  },
});

export const tasksReducer = tasksSlice.reducer;
