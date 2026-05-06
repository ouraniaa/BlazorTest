using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace Domain.Models;
 
public class User : Entity
{
    public int Id{ get; set; }
    public string Name{ get; set; }
    public string LastName{ get; set; }
    public string PhoneNumber{ get; set; }
    public string Email{ get; set; }

    public string AFM{ get; set; }
    public string Password { get; set; }

    public int? CompanyId{ get; set; }
    public List<UserRoles> UserRoles { get; set; }

    public int? DepartmentId { get; set; }
    virtual public Department? Department { get; set; } = default;
    virtual public Company? Company { get; set; } = default;
    virtual public List<Nickname> Nicknames { get; set; } = new List<Nickname>();
    public User UpdateValues(User user)
    {
        this.Name = user.Name;
        this.LastName = user.LastName;
        this.PhoneNumber = user.PhoneNumber;
        this.Email = user.Email;
        this.AFM = user.AFM;
        this.CompanyId = user.CompanyId;
        this.DepartmentId = user.DepartmentId;
        return this;
    }

}