import React from 'react';
import "./todoitem.css";
import "bootstrap-icons/font/bootstrap-icons.css";
import { IconButton } from '@mui/material';

function TodoItem({ task, deleteTask, toggleCompleted }) {
    function handleChange() {
        toggleCompleted(task.id);
    }

    return (
        <div className="todo-item">
            <div className="todo-content">
                <input
                    type="checkbox"
                    checked={task.completed}
                    onChange={handleChange}
                    className="todo-checkbox"
                />
                <div className="todo-details">
                    <span className="todo-title">{task.title}</span>
                    <span className="todo-description">{task.description || 'No description available'}</span>
                    <span className="todo-deadline">Deadline: {new Date(task.deadLine).toLocaleString()}</span>
                </div>
            </div>
            <div className="todo-actions">
                <IconButton aria-label="edit" size="small" color="dark" onClick={() => deleteTask(task.id)}>
                    <i class="bi bi-pencil-square"></i>
                </IconButton>
                <IconButton aria-label="delete" size="small" color="dark" onClick={() => deleteTask(task.id)}>
                    <i className="bi bi-trash"></i>
                </IconButton>
            </div>
        </div>
    );
}

export default TodoItem;
