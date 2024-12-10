import "./tasks.css";
import React, { useState, useEffect } from "react";
import TodoItem from "../../components/todoitem/TodoItem";
import { fetchData } from "../../functions/fetchData"; // Assuming fetchData is imported
import { useRootContext } from "../../hooks/useRootContext";
import "bootstrap-icons/font/bootstrap-icons.css";
import Modal from "@mui/material/Modal";
import { Button, IconButton } from "@mui/material";
import Box from "@mui/material/Box";

const style = {
    position: "absolute",
    top: "50%",
    left: "50%",
    transform: "translate(-50%, -50%)",
    width: "90%",
    maxWidth: 500,
    backgroundColor: "#fff",
    border: "2px solid #ddd",
    borderRadius: "8px",
    boxShadow: "0 4px 8px rgba(0,0,0,0.1)",
    padding: "20px",
};

function Tasks() {
    const [open, setOpen] = useState(false);
    const { baseUrl } = useRootContext();
    const [tasks, setTasks] = useState([]);
    const [title, setTitle] = useState("");
    const [description, setDescription] = useState("");
    const [deadLine, setDeadLine] = useState("");

    useEffect(() => {
        async function fetchTasks() {
            try {
                const response = await fetchData({
                    url: baseUrl + "/api/todos",
                    method: "GET",
                });

                if (response?.data) {
                    setTasks(response.data);
                }
            } catch (error) {
                console.error("Failed to fetch tasks", error);
            }
        }

        fetchTasks();
    }, [baseUrl]);

    async function addTask() {
        if (!title || !deadLine) {
            alert("Please fill all fields.");
            return;
        }

        const newTask = { title, description, deadLine };

        try {
            const response = await fetchData({
                url: baseUrl + "/api/todos",
                method: "POST",
                data: newTask,
            });

            if (response?.code === 200) {
                setTasks([...tasks, response.data]);
                setTitle("");
                setDescription("");
                setDeadLine("");
                setOpen(false);
            } else {
                alert("Failed to create task");
            }
        } catch (error) {
            console.error("Error adding task", error);
        }
    }

    async function deleteTask(id) {
        try {
            const response = await fetchData({
                url: baseUrl + `/api/todos/${id}`,
                method: "DELETE",
            });

            if (response?.code === 200) {
                setTasks(tasks.filter(task => task.id !== id));
            } else {
                alert("Failed to delete task");
            }
        } catch (error) {
            console.error("Error deleting task", error);
        }
    }

    async function toggleCompleted(id) {
        const task = tasks.find(task => task.id === id);
        const updatedTask = { ...task, completed: !task.completed };

        try {
            const response = await fetchData({
                url: baseUrl + `/api/todos/${id}`,
                method: "PATCH",
                data: updatedTask,
            });

            if (response?.code === 200) {
                setTasks(tasks.map(task => (task.id === id ? updatedTask : task)));
            } else {
                alert("Failed to update task");
            }
        } catch (error) {
            console.error("Error updating task", error);
        }
    }

    return (
        <div className="todo-list">
            <div className="todo-header">
                <Button variant="contained" color="success" size="small" onClick={() => setOpen(true)} className="add-task-button">
                    Add Task
                </Button>
            </div>

            {tasks?.length > 0 ? (
                tasks.map(task => (
                    <TodoItem
                        key={task.id}
                        task={task}
                        deleteTask={deleteTask}
                        toggleCompleted={toggleCompleted}
                    />
                ))
            ) : (
                <p>No tasks available</p>
            )}

            <Modal open={open} onClose={() => setOpen(false)}>
                <Box sx={style}>
                    <h3 className="modal-title">Create New Task</h3>
                    <div className="form-group">
                        <label>Title</label>
                        <input
                            type="text"
                            className="form-control"
                            value={title}
                            onChange={e => setTitle(e.target.value)}
                            placeholder="Enter task title"
                        />
                    </div>
                    <div className="form-group">
                        <label>Description</label>
                        <textarea
                            className="form-control"
                            value={description}
                            onChange={e => setDescription(e.target.value)}
                            placeholder="Enter task description"
                        />
                    </div>
                    <div className="form-group">
                        <label>Deadline</label>
                        <input
                            type="datetime-local"
                            className="form-control"
                            value={deadLine}
                            onChange={e => setDeadLine(e.target.value)}
                        />
                    </div>
                    <div className="modal-actions">
                        <Button variant="contained" color="secondary" size="small" onClick={() => setOpen(false)}>Close</Button>
                        <Button variant="contained" color="success" size="small" onClick={addTask}>Add</Button>
                    </div>
                </Box>
            </Modal>
        </div>
    );
}

export default Tasks;
