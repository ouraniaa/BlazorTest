
using Application.Interfaces;
using Application.Utilities;
using Domain.IRepositories;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace Application.Services;


public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ICompanyService _companyService;
    private readonly INicknameService _nicknameService;

    public UserService(IUserRepository userRepository,ICompanyService companyService, INicknameService nicknameService)
    {
        _userRepository = userRepository;        
        _companyService = companyService;
        _nicknameService = nicknameService;
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
            string afmError = AFMValidator.ValidateVatNumber(user.AFM);
            if (afmError != null)
            {
                throw new Exception("The AFM provided is invalid. Please check the 9 digits.");

            }
            if (user.CompanyId != null)
            {
                var company = await _companyService.GetCompanyByID((int)user.CompanyId);

                Console.WriteLine(company.Name);

            }
            User newUser = new User
            {
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                AFM = user.AFM,
                PhoneNumber = user.PhoneNumber,
                Company = user.Company,
                CompanyId = user.CompanyId
            };

            _userRepository.Add(newUser);
            await _userRepository.SaveChangesAsync();

            foreach (var item in user.Nicknames)
            {
                item.UserId = newUser.Id;
                await _nicknameService.Save(item);

            }

            return newUser;

        }
        catch (Exception ex)
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
            existingUser.CompanyId = user.CompanyId;

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
