import { useEffect, useState } from 'react';
import { useAppDispatch, useAppSelector } from '@/app/hooks';
import TaskForm from './TaskForm';
import type { TaskReadDto } from '../dto/task-read';
import {
  selectTasks,
  selectTasksError,
  selectTasksLoading,
} from '../slice/selectors';
import { fetchTasks, deleteTaskThunk } from '../thunks/tasks-thunk';
import { TaskStatusLabel } from '../enums/task-status';
import { TaskPriorityLabel } from '../enums/task-priority';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import {
  faPen,
  faTrash,
  faArrowUpShortWide,
  faArrowDownWideShort,
  faPlus,
  faMagnifyingGlass,
} from '@fortawesome/free-solid-svg-icons';

export default function TaskList() {
  const dispatch = useAppDispatch();
  const tasks = useAppSelector(selectTasks);
  const loading = useAppSelector(selectTasksLoading);
  const error = useAppSelector(selectTasksError);
  const [q, setQ] = useState('');
  const [sort, setSort] = useState<'asc' | 'desc'>('asc');
  const [editingTask, setEditingTask] = useState<TaskReadDto | null>(null);

  useEffect(() => {
    dispatch(fetchTasks({ sort }));
  }, [dispatch]);

  const handleSearch = () => {
    dispatch(fetchTasks({ q, sort }));
  };

  const handleSort = (dir: 'asc' | 'desc') => {
    setSort(dir);
    dispatch(fetchTasks({ q, sort: dir }));
  };

  if (loading) {
    return <p className="p-6 text-gray-600">Loading tasks…</p>;
  }

  return (
    <div className="space-y-6 p-6 bg-gray-50 min-h-screen">
      <div className="flex flex-wrap items-center justify-between gap-4">
        <h2 className="text-3xl font-bold text-gray-800">Task List</h2>

        <div className="flex items-center gap-2">
          <button
            className={`p-2 rounded-lg border transition
              ${sort === 'asc'
                ? 'bg-blue-600 text-white border-blue-600'
                : 'bg-white hover:bg-gray-100'}`}
            onClick={() => handleSort('asc')}
          >
            <FontAwesomeIcon icon={faArrowUpShortWide} />
          </button>

          <button
            className={`p-2 rounded-lg border transition
              ${sort === 'desc'
                ? 'bg-blue-600 text-white border-blue-600'
                : 'bg-white hover:bg-gray-100'}`}
            onClick={() => handleSort('desc')}
          >
            <FontAwesomeIcon icon={faArrowDownWideShort} />
          </button>

          <button
            className="px-4 py-2 rounded-lg bg-green-600 text-white hover:bg-green-700 transition"
            onClick={() => setEditingTask({} as TaskReadDto)}
          >
            <FontAwesomeIcon icon={faPlus} className="mr-2" />
            New Task
          </button>
        </div>
      </div>

      {/* Search */}
      <div className="flex items-center gap-2 max-w-md">
        <input
          className="w-full px-4 py-2 border rounded-lg bg-white focus:outline-none focus:ring-2 focus:ring-blue-500"
          placeholder="Search by title or description…"
          value={q}
          onChange={(e) => setQ(e.target.value)}
        />

        <button
          className="px-4 py-2 rounded-lg bg-blue-600 text-white hover:bg-blue-700 transition"
          onClick={handleSearch}
        >
          <FontAwesomeIcon icon={faMagnifyingGlass} />
        </button>
      </div>

      {error && (
        <div className="bg-red-100 text-red-700 border border-red-300 px-4 py-3 rounded-lg">
          {error}
        </div>
      )}

      <div className="bg-white rounded-2xl shadow-sm border overflow-x-auto">
        <table className="w-full text-sm table-fixed">
          <thead className="bg-gray-100 text-gray-600 uppercase text-xs tracking-wide">
            <tr>
              <th className="px-4 py-3 text-left w-1/5">Title</th>
              <th className="px-4 py-3 text-left w-2/5">Description</th>
              <th className="px-4 py-3 text-center w-28 whitespace-nowrap">Status</th>
              <th className="px-4 py-3 text-center w-24 whitespace-nowrap">Priority</th>
              <th className="px-4 py-3 text-center w-28 whitespace-nowrap">Due Date</th>
              <th className="px-4 py-3 text-center w-28 whitespace-nowrap">Created</th>
              <th className="px-4 py-3 text-center w-28 whitespace-nowrap">Actions</th>
            </tr>
          </thead>

          <tbody>
            {tasks.map((task) => (
              <tr
                key={task.id}
                className="border-t hover:bg-gray-50 transition"
              >
                <td className="px-4 py-3 font-semibold text-gray-800 truncate">
                  {task.title}
                </td>

                <td className="px-4 py-3 text-gray-600 truncate">
                  {task.description || '—'}
                </td>

                <td className="px-4 py-3 text-center whitespace-nowrap">
                  <span className="px-3 py-1 text-xs font-medium bg-blue-100 text-blue-700 rounded-full">
                    {TaskStatusLabel[task.status]}
                  </span>
                </td>

                <td className="px-4 py-3 text-center whitespace-nowrap">
                  <span className="px-3 py-1 text-xs font-medium bg-gray-200 text-gray-700 rounded-full">
                    {TaskPriorityLabel[task.priority]}
                  </span>
                </td>

                <td className="px-4 py-3 text-center whitespace-nowrap text-gray-700">
                  {task.dueDate?.split('T')[0] || '—'}
                </td>

                <td className="px-4 py-3 text-center whitespace-nowrap text-gray-500">
                  {task.createdAt.split('T')[0]}
                </td>

                <td className="px-4 py-3 text-center whitespace-nowrap">
                  <div className="flex justify-center gap-2">
                    <button
                      className="p-2 rounded-lg border text-blue-600 hover:bg-blue-50 transition"
                      onClick={() => setEditingTask(task)}
                    >
                      <FontAwesomeIcon icon={faPen} />
                    </button>

                    <button
                      className="p-2 rounded-lg border text-red-600 hover:bg-red-50 transition"
                      onClick={() => {
                        if (window.confirm('Delete this task?')) {
                          dispatch(deleteTaskThunk(task.id));
                        }
                      }}
                    >
                      <FontAwesomeIcon icon={faTrash} />
                    </button>
                  </div>
                </td>
              </tr>
            ))}

            {tasks.length === 0 && (
              <tr>
                <td
                  colSpan={7}
                  className="px-4 py-6 text-center text-gray-500"
                >
                  No tasks found
                </td>
              </tr>
            )}
          </tbody>
        </table>
      </div>

      {editingTask && (
        <TaskForm
          task={editingTask.id ? editingTask : null}
          onCancel={() => setEditingTask(null)}
        />
      )}
    </div>
  );
}
