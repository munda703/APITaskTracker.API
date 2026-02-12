// import { useEffect, useState, useRef } from 'react';
// import { useAppDispatch } from '@/app/hooks';
// import type { TaskReadDto } from '../dto/task-read';
// import { TaskPriority, TaskPriorityLabel } from '../enums/task-priority';
// import { TaskStatus, TaskStatusLabel } from '../enums/task-status';
// import { updateTaskThunk, createTaskThunk } from '../thunks/tasks-thunk';


// interface Props {
//     task?: TaskReadDto | null;
//     onCancel: () => void;
// }

// export default function TaskForm({ task, onCancel }: Props) {

//     const dispatch = useAppDispatch();
//     const [title, setTitle] = useState('');
//     const [description, setDescription] = useState('');
//     const [priority, setPriority] = useState<TaskPriority>(TaskPriority.Low);
//     const [status, setStatus] = useState<TaskStatus>(TaskStatus.New);
//     const [dueDate, setDueDate] = useState('');
//     const [errors, setErrors] = useState<Record<string, string[]>>({});

//     const isEdit = Boolean(task);

//     useEffect(() => {
//         if (task) {
//             setTitle(task.title);
//             setDescription(task.description ?? '');
//             setPriority(task.priority);
//             setStatus(task.status);
//             setDueDate(task.dueDate?.split('T')[0] ?? '');
//         }
//     }, [task]);

//     const submit = () => {
//         if (!title.trim()) return;

//         if (isEdit && task) {
//             dispatch(
//                 updateTaskThunk({
//                     id: task.id,
//                     dto: {
//                         title,
//                         description,
//                         priority,
//                         status,
//                         dueDate: dueDate || undefined,
//                     },
//                 })
//             );
//         } else {
//             dispatch(
//                 createTaskThunk({
//                     title,
//                     description,
//                     priority,
//                     dueDate: dueDate || undefined,
//                 })
//             );
//         }

//         onCancel();
//     };

//     return (
//         <div className="bg-white border rounded-lg shadow p-6 mb-6">
//             <h2 className="text-2xl font-semibold mb-6 text-center">
//                 {isEdit ? 'Edit Task' : 'Create Task'}
//             </h2>

//             <div className="space-y-5">

//                 <div>
//                     <label className="block mb-1 font-medium">
//                         Title
//                     </label>
//                     <input
//                         className="w-full px-3 py-2 border rounded focus:outline-none focus:ring"
//                         placeholder="Enter task title"
//                         value={title}
//                         onChange={e => setTitle(e.target.value)}
//                     />
//                 </div>

//                 <div>
//                     <label className="block mb-1 font-medium">
//                         Description
//                     </label>
//                     <textarea
//                         className="w-full px-3 py-2 border rounded resize-none overflow-y-auto focus:outline-none focus:ring"
//                         rows={4}
//                         placeholder="Enter task description"
//                         value={description}
//                         onChange={e => setDescription(e.target.value)}
//                     />

//                 </div>

//                 {/* <select
//                     className="w-full px-3 py-2 border rounded focus:outline-none focus:ring"
//                     value={status}
//                     onChange={e =>
//                         setStatus(Number(e.target.value) as TaskStatus)
//                     }
//                 >
//                     {Object.values(TaskStatus).map(v => (
//                         <option key={v} value={v}>
//                             {TaskStatusLabel[v]}
//                         </option>
//                     ))}
//                 </select> */}

//                 {isEdit && (
//                     <div>
//                         <label className="block mb-1 font-medium">
//                             Status
//                         </label>
//                         <select
//                             className="w-full px-3 py-2 border rounded focus:outline-none focus:ring"
//                             value={status}
//                             onChange={e =>
//                                 setStatus(Number(e.target.value) as TaskStatus)
//                             }
//                         >
//                             {Object.values(TaskStatus).map(v => (
//                                 <option key={v} value={v}>
//                                     {TaskStatusLabel[v]}
//                                 </option>
//                             ))}
//                         </select>
//                     </div>
//                 )}

//                 <select
//                     className="w-full px-3 py-2 border rounded"
//                     value={priority}
//                     onChange={e =>
//                         setPriority(Number(e.target.value) as TaskPriority)
//                     }
//                 >
//                     {Object.values(TaskPriority).map(v => (
//                         <option key={v} value={v}>
//                             {TaskPriorityLabel[v]}
//                         </option>
//                     ))}
//                 </select>
//                 <div className="relative inline-block">
//                     <input
//                         type="date"
//                         className="
//                         w-[180px]
//                         px-3 py-2 pr-10   
//                         border rounded
//                         focus:outline-none focus:ring
//                         cursor-pointer
//                         "
//                         value={dueDate ?? ''}
//                         onChange={e => setDueDate(e.target.value)}
//                     />
//                 </div>

//             </div>

//             <div className="flex justify-end gap-3 mt-6">
//                 <button
//                     className="px-4 py-2 border rounded"
//                     onClick={onCancel}
//                 >
//                     Cancel
//                 </button>
//                 <button
//                     className="px-5 py-2 bg-blue-600 text-white rounded"
//                     onClick={submit}
//                 >
//                     {isEdit ? 'Update' : 'Create'}
//                 </button>
//             </div>
//         </div>
//     );
// }


import { useEffect, useState } from 'react';
import { useAppDispatch } from '@/app/hooks';
import type { TaskReadDto } from '../dto/task-read';
import { TaskPriority, TaskPriorityLabel } from '../enums/task-priority';
import { TaskStatus, TaskStatusLabel } from '../enums/task-status';
import { updateTaskThunk, createTaskThunk } from '../thunks/tasks-thunk';

interface Props {
    task?: TaskReadDto | null;
    onCancel: () => void;
}

export default function TaskForm({ task, onCancel }: Props) {
    const dispatch = useAppDispatch();

    const [title, setTitle] = useState('');
    const [description, setDescription] = useState('');
    const [priority, setPriority] = useState<TaskPriority>(TaskPriority.Low);
    const [status, setStatus] = useState<TaskStatus>(TaskStatus.New);
    const [dueDate, setDueDate] = useState('');
    const [errors, setErrors] = useState<Record<string, string[]>>({});

    const isEdit = Boolean(task);

    useEffect(() => {
        if (task) {
            setTitle(task.title);
            setDescription(task.description ?? '');
            setPriority(task.priority);
            setStatus(task.status);
            setDueDate(task.dueDate?.split('T')[0] ?? '');
        }
    }, [task]);

    const submit = async () => {
        setErrors({}); // clear previous errors

        let result;

        if (isEdit && task) {
            result = await dispatch(
                updateTaskThunk({
                    id: task.id,
                    dto: {
                        title,
                        description,
                        priority,
                        status,
                        dueDate: dueDate || undefined,
                    },
                })
            );

            if (updateTaskThunk.rejected.match(result)) {
                setErrors(result.payload?.errors ?? {});
                return;
            }
        } else {
            result = await dispatch(
                createTaskThunk({
                    title,
                    description,
                    priority,
                    dueDate: dueDate || undefined,
                })
            );

            if (createTaskThunk.rejected.match(result)) {
                setErrors(result.payload?.errors ?? {});
                return;
            }
        }

        // only close form if success
        onCancel();
    };

    return (
        <div className="bg-white border rounded-lg shadow p-6 mb-6">
            <h2 className="text-2xl font-semibold mb-6 text-center">
                {isEdit ? 'Edit Task' : 'Create Task'}
            </h2>

            {/* Global danger message */}
            {Object.keys(errors).length > 0 && (
                <div className="bg-red-100 text-red-700 border border-red-300 px-4 py-2 rounded mb-4">
                    Please fix the errors below.
                </div>
            )}

            <div className="space-y-5">
                {/* Title */}
                <div>
                    <label className="block mb-1 font-medium">
                        Title
                    </label>

                    <input
                        className={`w-full px-3 py-2 border rounded focus:outline-none focus:ring
                            ${errors.Title ? 'border-red-500' : ''}`}
                        placeholder="Enter task title"
                        value={title}
                        onChange={e => {
                            setTitle(e.target.value);

                            // clear title error while typing
                            if (errors.Title) {
                                const { Title, ...rest } = errors;
                                setErrors(rest);
                            }
                        }}
                    />

                    {errors.Title && (
                        <span className="text-red-600 text-sm mt-1 block">
                            {errors.Title[0]}
                        </span>
                    )}
                </div>

                {/* Description */}
                <div>
                    <label className="block mb-1 font-medium">
                        Description
                    </label>
                    <textarea
                        className="w-full px-3 py-2 border rounded resize-none overflow-y-auto focus:outline-none focus:ring"
                        rows={4}
                        placeholder="Enter task description"
                        value={description}
                        onChange={e => setDescription(e.target.value)}
                    />
                </div>

                {/* Status (edit only) */}
                {isEdit && (
                    <div>
                        <label className="block mb-1 font-medium">
                            Status
                        </label>
                        <select
                            className="w-full px-3 py-2 border rounded focus:outline-none focus:ring"
                            value={status}
                            onChange={e =>
                                setStatus(Number(e.target.value) as TaskStatus)
                            }
                        >
                            {Object.values(TaskStatus).map(v => (
                                <option key={v} value={v}>
                                    {TaskStatusLabel[v]}
                                </option>
                            ))}
                        </select>
                    </div>
                )}

                {/* Priority */}
                <div>
                    <label className="block mb-1 font-medium">
                        Priority
                    </label>
                    <select
                        className="w-full px-3 py-2 border rounded"
                        value={priority}
                        onChange={e =>
                            setPriority(Number(e.target.value) as TaskPriority)
                        }
                    >
                        {Object.values(TaskPriority).map(v => (
                            <option key={v} value={v}>
                                {TaskPriorityLabel[v]}
                            </option>
                        ))}
                    </select>
                </div>

                {/* Due date */}
                <div>
                    <label className="block mb-1 font-medium">
                        Due Date
                    </label>
                    <input
                        type="date"
                        className="w-[180px] px-3 py-2 border rounded focus:outline-none focus:ring cursor-pointer"
                        value={dueDate}
                        onChange={e => setDueDate(e.target.value)}
                    />
                </div>
            </div>

            <div className="flex justify-end gap-3 mt-6">
                <button
                    className="px-4 py-2 border rounded"
                    onClick={onCancel}
                >
                    Cancel
                </button>
                <button
                    className="px-5 py-2 bg-blue-600 text-white rounded"
                    onClick={submit}
                >
                    {isEdit ? 'Update' : 'Create'}
                </button>
            </div>
        </div>
    );
}


