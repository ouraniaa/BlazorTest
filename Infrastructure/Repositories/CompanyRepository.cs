using Domain.IRepositories;
using Domain.Models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories;

public class CompanyRepository : UnitOfWork, ICompanyRepository
{
    
    public CompanyRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
    public async Task<List<Company>> GetCompanies()
    {
        return await _dbContext.Companies.ToListAsync();
    }

    public async Task<Company?> GetCompanyWithUsers(int companyId)
    {
        return await _dbContext.Companies
            .Include(c => c.Users)
            .FirstOrDefaultAsync(c => c.Id == companyId);
    }

    public async Task<List<User>> GetEmployeesByCompanyId(int companyId)
    {
        try
        {
            return await _dbContext.Users
                .Where(u => u.CompanyId == companyId)
                .ToListAsync();
        }
        catch (Exception ex)
        {            
            throw new Exception($"Error in Repository: {ex.Message}", ex);
        }
    }

    public async Task<Company?> GetCompanyById(int companyId)
    {
        return await _dbContext.Companies.FirstOrDefaultAsync(x => x.Id == companyId);
    }

}
