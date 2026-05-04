using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models;

public class Entity
{
    public string?  CreationUser{ get; set; }
    public DateTime? CreationDate{ get; set; }
    public string?  UpdateUser{ get; set; }
    public DateTime? UpdateDate{ get; set; }
}
