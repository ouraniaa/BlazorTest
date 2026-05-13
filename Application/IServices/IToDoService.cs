using Domain.Models;

namespace Application.Interfaces;

public interface IToDoService
{
    Task<List<ToDoList>> GetUserTodoListsAsync(int userId);
    Task<ToDoList?> GetListWithTasksAsync(int listId);
    Task CreateListAsync(string name, int userId);
    Task AddTaskToListAsync(int listId, string taskTitle);
    Task ToggleTaskStatusAsync(int taskId);
    Task DeleteTaskAsync(int taskId);
    Task DeleteListAsync(int listId);
}