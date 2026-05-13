using Domain.IRepositories;
using Domain.Models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ToDoRepository : UnitOfWork, IToDoRepository
{
    public ToDoRepository(AppDbContext dbContext) : base(dbContext)
    {
    }


    public async Task<List<ToDoList>> GetUserTodoListsAsync(int userId)
    {
        return await _dbContext.TodoLists
            .Include(tl => tl.Tasks)
            .Where(tl => tl.UserId == userId)
            .ToListAsync();
    }

    public async Task<ToDoList?> GetListWithTasksAsync(int listId)
    {
        return await _dbContext.TodoLists
            .Include(tl => tl.Tasks)
            .FirstOrDefaultAsync(tl => tl.Id == listId);
    }

    public async Task AddListAsync(ToDoList list)
    {
        await _dbContext.TodoLists.AddAsync(list);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteListAsync(int listId)
    {
        var list = await _dbContext.TodoLists.FindAsync(listId);
        if (list != null)
        {
            _dbContext.TodoLists.Remove(list);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<TaskItem?> GetTaskByIdAsync(int taskId)
    {
        return await _dbContext.TaskItems.FindAsync(taskId);
    }

    public async Task AddTaskAsync(TaskItem task)
    {
        await _dbContext.TaskItems.AddAsync(task);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateTaskAsync(TaskItem task)
    {
        _dbContext.TaskItems.Update(task);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteTaskAsync(int taskId)
    {
        var task = await _dbContext.TaskItems.FindAsync(taskId);
        if (task != null)
        {
            _dbContext.TaskItems.Remove(task);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}