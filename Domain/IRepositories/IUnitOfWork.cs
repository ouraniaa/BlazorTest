using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.IRepositories;

public interface IUnitOfWork : IDisposable
{
    void Add(object entity);
    void AddRange(IEnumerable<object> entity);
    void Remove(object entity);
    void RemoveRange(IEnumerable<object> entity);
    //
    void SetAsModifield(object entity);
    //
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);   

    void BeginTransaction();
    void Commit();
    void Rollback();
}
