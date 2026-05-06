using System;
using System.Collections.Generic;
using System.Text;
using Application.Interfaces;
using Domain.IRepositories;
using Domain.Models;

namespace Application.Services;

public class NicknameService : INicknameService
{
    private readonly INicknameRepository _nicknameRepository;

    public NicknameService(INicknameRepository nicknameRepository)
    {
        _nicknameRepository = nicknameRepository;
    }

    public async Task DeleteById(int nicknameId)
    {
        try
        {
            var existingNickname = await _nicknameRepository.GetNicknameById(nicknameId);
            if (existingNickname == null)
                throw new Exception("Nickname not found");

            _nicknameRepository.Remove(existingNickname);
            await _nicknameRepository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<List<Nickname>> GetNicknamesByUserId(int userId)
    {
        try
        {
            return await _nicknameRepository.GetNicknamesByUserId(userId);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<Nickname?> GetNicknameById(int nicknameId)
    {
        return await _nicknameRepository.GetNicknameById(nicknameId);
    }

    public async Task<Nickname> Save(Nickname nickname)
    {
  
        _nicknameRepository.Add(nickname);
        await _nicknameRepository.SaveChangesAsync();
        return nickname;
    }

    public async Task<Nickname> Update(Nickname nickname)
    {
        try
        {
            var existingNickname = await _nicknameRepository.GetNicknameById(nickname.Id);
            if (existingNickname == null)
                throw new Exception("Nickname not found");

            existingNickname.UpdateValues(nickname);
            await _nicknameRepository.SaveChangesAsync();

            return existingNickname;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
