
using Application.Interfaces;
using Domain.IRepositories;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services;


public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ICompanyService _companyService;
    

    public UserService(IUserRepository userRepository,ICompanyService companyService)
    {
        _userRepository = userRepository;        
        _companyService = companyService;
    }

    public Task<User> DuplicateUser(User user, User user2)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUser(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<User> Save(User user)
    {
        try
        {
            if (user.CompanyId != null)
            {
               var company= await _companyService.GetCompanyByID((int)user.CompanyId);
                Console.WriteLine(company.Name);

            }
            _userRepository.Add(user);

            await _userRepository.SaveChangesAsync();
            return user;

        }catch(Exception ex)
        {
            throw;
        }
    }


    public async Task<User> Update(User user)
    {
        try
        {
            var existingUser = await _userRepository.GetUserById(user.Id);
            if (existingUser == null)
                throw new Exception("Exception");
            existingUser.UpdateValues(user);             
            await _userRepository.SaveChangesAsync();

            return existingUser;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await _userRepository.GetAll();
    }

    public async Task<User?> GetUserByID(int userId)
    {
        try
        {
            var existingUser = await _userRepository.GetUserById(userId);
            if (existingUser == null)
                throw new Exception("Exception");          

            return existingUser;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task DeleteById(int userId)
    {
        try
        {
            var existingUser = await _userRepository.GetUserById(userId);
            if (existingUser == null)
                throw new Exception("Exception");

            _userRepository.Remove(existingUser);
            await _userRepository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
