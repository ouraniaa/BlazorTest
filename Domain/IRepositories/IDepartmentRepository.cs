using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.IRepositories;

public interface IDepartmentRepository : IUnitOfWork
{
   Task<Department?> GetByDepartmentId(int id);
   Task<List<Department>> GetByCompanyId(int companyId);
}