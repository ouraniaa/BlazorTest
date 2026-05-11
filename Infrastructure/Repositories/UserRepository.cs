using Domain.IRepositories;
using Domain.Models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories;

public class UserRepository : UnitOfWork, IUserRepository
{
    public UserRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
      
   
    public async Task<List<User>> GetAll()
    {
        return await _dbContext.Users
                               .Include(u => u.Nicknames)
                               .ToListAsync();
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await _dbContext.Users
                .Include(x => x.UserRoles)
                    .ThenInclude(ur => ur.Role)
               .FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<User?> GetUserById(int userId)
    {
        return await _dbContext.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();
    }
}
