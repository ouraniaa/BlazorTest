using Application.Interfaces;
using Domain.IRepositories;
using Domain.Models;

namespace Application.Services;

public class ToDoService : IToDoService
{
    private readonly IToDoRepository _todoRepository;

    public ToDoService(IToDoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public async Task<List<ToDoList>> GetUserTodoListsAsync(int userId)
    {
        return await _todoRepository.GetUserTodoListsAsync(userId);
    }

    public async Task<ToDoList?> GetListWithTasksAsync(int listId)
    {
        return await _todoRepository.GetListWithTasksAsync(listId);
    }

    public async Task CreateListAsync(string name, int userId)
    {
        var newList = new ToDoList
        {
            Name = name,
            UserId = userId
        };
        await _todoRepository.AddListAsync(newList);
    }

    public async Task AddTaskToListAsync(int listId, string taskTitle)
    {
        var newTask = new TaskItem
        {
            Title = taskTitle,
            TodoListId = listId,
            IsCompleted = false
        };
        await _todoRepository.AddTaskAsync(newTask);
    }

    public async Task ToggleTaskStatusAsync(int taskId)
    {
        var task = await _todoRepository.GetTaskByIdAsync(taskId);
        if (task != null)
        {
            task.IsCompleted = !task.IsCompleted;
            await _todoRepository.UpdateTaskAsync(task);
        }
    }

    public async Task DeleteTaskAsync(int taskId)
    {
        await _todoRepository.DeleteTaskAsync(taskId);
    }

    public async Task DeleteListAsync(int listId)
    {
        await _todoRepository.DeleteListAsync(listId);
    }
}