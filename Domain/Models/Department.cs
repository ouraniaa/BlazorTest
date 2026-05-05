using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace Domain.Models;

public class Department : Entity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public int CompanyId { get; set; }
    virtual public Company? Company { get; set; } = default;

    public List<User> Users { get; set; } = new List<User>();

    public Department UpdateValues(Department department)
    {
        this.Name = department.Name;
        this.CompanyId = department.CompanyId;
        return this;
    }
}