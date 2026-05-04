using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.IRepositories;

public interface IUserRepository : IUnitOfWork
{
    public Task<User?> GetUserById(int userId);
    Task<List<User>> GetAll();  
}
