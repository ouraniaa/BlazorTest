using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models;

public class Nickname
{

    public int Id { get; set; }
    public string Value { get; set; } = string.Empty;
    public int UserId { get; set; }
    virtual public User? User { get; set; } = default;

    public Nickname UpdateValues(Nickname nickname)
    {
        this.Value = nickname.Value;
        this.UserId = nickname.UserId;
        return this;
    }
}
