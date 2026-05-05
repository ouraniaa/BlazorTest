using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.IRepositories;

public interface INicknameRepository : IUnitOfWork
{
    Task<Nickname?> GetNicknameById(int nicknameId);
    Task<List<Nickname>> GetNicknamesByUserId(int userId);
   // void Add(Nickname nickname);
    //void Remove(Nickname nickname);
}
