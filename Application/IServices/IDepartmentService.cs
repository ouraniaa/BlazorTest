using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.IServices;

public interface IDepartmentService
{
    Task<List<Department>> GetAll();
}
