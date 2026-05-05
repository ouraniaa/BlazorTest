using System;
using System.Collections.Generic;
using System.Text;
using Domain.IRepositories;
using Domain.Models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class NicknameRepository: UnitOfWork, INicknameRepository
{
    public NicknameRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
    public async Task<List<Nickname>> GetNicknamesByUserId(int userId)
    {
        return await _dbContext.Nicknames
            .Where(x => x.UserId == userId)
            .ToListAsync();
    }
    public async Task<Nickname?> GetNicknameById(int nicknameId)
    {
        return await _dbContext.Nicknames.FirstOrDefaultAsync(x => x.Id == nicknameId);
    }
}
