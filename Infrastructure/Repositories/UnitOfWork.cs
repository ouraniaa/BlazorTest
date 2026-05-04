using Domain.IRepositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    //
    protected AppDbContext _dbContext;
    //
    private IDbContextTransaction? _uow;

    public UnitOfWork(AppDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public void Add(object entity)
    {
        this._dbContext.Add(entity);
    }

    public void AddRange(IEnumerable<object> entity)
    {
        this._dbContext.AddRange(entity);
    }

    public void Remove(object entity)
    {
        this._dbContext.Remove(entity);
    }

    public void RemoveRange(IEnumerable<object> entity)
    {
        this._dbContext.RemoveRange(entity);
    }

    public void SetAsModifield(object entity)
    {
        this._dbContext.Entry(entity).State = EntityState.Modified;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        //
        int res = await this._dbContext.SaveChangesAsync(cancellationToken);
        //
        return res;
    }     
  
    public void BeginTransaction()
    {
        this._uow = this._dbContext.Database.BeginTransaction();
    }

    public void Commit()
    {
        this._uow?.Commit();
    }

    public void Rollback()
    {
        this._uow?.Rollback();
    }

    public void Dispose()
    {
        this._uow?.Dispose();
    }
}
