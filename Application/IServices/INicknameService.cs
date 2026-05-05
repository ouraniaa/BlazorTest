using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface INicknameService
{
    public Task<Nickname> Save(Nickname nickname);
    Task<Nickname> Update(Nickname nickname);
    public Task DeleteById(int nicknameId);
    
    Task<Nickname?> GetNicknameById(int nicknameId);
    Task<List<Nickname>> GetNicknamesByUserId(int userId);
}