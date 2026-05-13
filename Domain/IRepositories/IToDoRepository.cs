using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.IRepositories;

public interface IToDoRepository : IUnitOfWork
{
    Task<List<ToDoList>> GetUserTodoListsAsync(int userId);
    Task<ToDoList?> GetListWithTasksAsync(int listId);
    Task AddListAsync(ToDoList list);
    Task DeleteListAsync(int listId);


    Task<TaskItem?> GetTaskByIdAsync(int taskId);
    Task AddTaskAsync(TaskItem task);
    Task UpdateTaskAsync(TaskItem task);
    Task DeleteTaskAsync(int taskId);
    Task SaveChangesAsync();
}