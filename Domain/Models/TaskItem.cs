using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; } = false;

    public int TodoListId { get; set; }
    public ToDoList? TodoList { get; set; }
}
