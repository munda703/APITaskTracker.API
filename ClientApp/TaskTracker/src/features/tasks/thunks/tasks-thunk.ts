import { createAsyncThunk } from "@reduxjs/toolkit";
import axios from "axios";
import { getTasks, createTask, updateTask, deleteTask } from "../api/tasks-api";
import type { TaskCreateDto } from "../dto/task-create";
import type { TaskReadDto } from "../dto/task-read";
import type { TaskUpdateDto } from "../dto/task-update";
import type { ApiError } from "./ApiError";

export const fetchTasks = createAsyncThunk<
    TaskReadDto[],
    { q?: string; sort?: 'asc' | 'desc' },
    { rejectValue: ApiError }
>(
    'tasks/fetchAll',
    async ({ q, sort = 'asc' }, { rejectWithValue }) => {
        try {

            return await getTasks(q, sort);
        }
        catch (error) {
            if (axios.isAxiosError(error)) {
                if (error.response?.data) {
                    return rejectWithValue(error.response.data);
                }

                return rejectWithValue({
                    title: 'Network error',
                    detail: 'Unable to reach the server',
                });
            }

            return rejectWithValue({
                title: 'Unexpected error',
                detail: 'Something went wrong',
            });
        }
    }
);

export const createTaskThunk = createAsyncThunk<TaskReadDto, TaskCreateDto, { rejectValue: any }>(
    'tasks/create',
    async (dto, { rejectWithValue }) => {
        try {
            return await createTask(dto);
        } catch (error) {
            if (axios.isAxiosError(error) && error.response?.data) {
                return rejectWithValue(error.response.data);
            }
            throw error;
        }
    }
);

export const updateTaskThunk = createAsyncThunk<
    TaskReadDto,
    { id: number; dto: TaskUpdateDto },
    { rejectValue: any }
>(
    'tasks/update',
    async ({ id, dto }, { rejectWithValue }) => {
        try {
            return await updateTask(id, dto);
        } catch (error) {
            if (axios.isAxiosError(error) && error.response?.data) {
                return rejectWithValue(error.response.data);
            }
            throw error;
        }
    }
);

export const deleteTaskThunk = createAsyncThunk<
    number,
    number,
    { rejectValue: any }>(
        'tasks/delete',
        async (id, { rejectWithValue }) => {
            try {
                await deleteTask(id);
                return id;
            } catch (error) {
                if (axios.isAxiosError(error) && error.response?.data) {
                    return rejectWithValue(error.response.data);
                }
                throw error;
            }
        }
    );
