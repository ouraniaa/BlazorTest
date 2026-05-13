using Application.Dtos.Auth;
using Application.Interfaces;
using Application.Utilities;
using Domain.IRepositories;
using Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Text;
namespace Application.Services;


public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ICompanyService _companyService;
    private readonly INicknameService _nicknameService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<UserService> logger;

    public UserService(IUserRepository userRepository,ICompanyService companyService, INicknameService nicknameService, IHttpContextAccessor httpContextAccessor, ILogger<UserService> logger)
    {
        _userRepository = userRepository;        
        _companyService = companyService;
        _nicknameService = nicknameService;
        _httpContextAccessor = httpContextAccessor;
        this.logger = logger;
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
            if (afmError != null) throw new Exception("Invalid AFM");

            if (!string.IsNullOrEmpty(user.Password))
            {
                user.Password = PasswordEncryption.HashPassword(user.Password);
            }

            User newUser = new User
            {
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                AFM = user.AFM,
                PhoneNumber = user.PhoneNumber,
                Password = user.Password,
                CompanyId = user.CompanyId,
                Nicknames = new List<Nickname>()
            };

            if (user.Nicknames != null)
            {
                foreach (var item in user.Nicknames)
                {
                    newUser.Nicknames.Add(new Nickname
                    {
                        Value = item.Value
                    });
                }
            }

            _userRepository.Add(newUser);
            await _userRepository.SaveChangesAsync();

            var defaultRole = new UserRoles { UserId = newUser.Id, RoleId = 9 };
            _userRepository.Add(defaultRole);
            await _userRepository.SaveChangesAsync();

            return newUser;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error saving user");
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

    public async Task<User> SignUp(SignUpDto signUpDto)
    {
        try
        {
            string? emailError = EmailValidator.ValidateEmail(signUpDto.Email);
            if (emailError != null)
                throw new Exception(emailError);

            string? afmError = AFMValidator.ValidateVatNumber(signUpDto.AFM);
            if (afmError != null)
                throw new Exception(afmError);

            var userByEmail = await _userRepository.GetUserByEmail(signUpDto.Email);
            if (userByEmail != null)
                throw new Exception("User already exists");


            string encryptedPassword = PasswordEncryption.HashPassword(signUpDto.Password);
            var newUser = new User()
            {
                Name = signUpDto.Name,
                LastName = signUpDto.LastName,
                Email = signUpDto.Email,
                AFM = signUpDto.AFM,
                PhoneNumber = signUpDto.PhoneNumber,
                Password = encryptedPassword
            };

            _userRepository.Add(newUser); 
            await _userRepository.SaveChangesAsync();

            var newRole = new UserRoles()
            {
                UserId = newUser.Id,
                RoleId = 7
            };

            _userRepository.Add(newRole);
            await _userRepository.SaveChangesAsync();

            return newUser;
        }

        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<User> Login(LoginModel loginDto)
    {
        try
        {
            var user = await _userRepository.GetUserByEmail(loginDto.Email);

            if (user == null) return null;
            

            bool isPasswordValid = PasswordEncryption.VerifyPassword(loginDto.Password, user.Password);

            if (!isPasswordValid)
            {
                return null;
            }

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email, ClaimValueTypes.String),
            new Claim(ClaimTypes.Role, user.UserRoles.FirstOrDefault()?.Role?.Name ?? "User", ClaimValueTypes.String),
            new Claim("CompanyId", user.CompanyId?.ToString() ?? "0")
        };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);                
            }

            return user;
        }
        catch(Exception ex)
        {
            logger.LogError("Login error: ", ex);
            throw;
        }
    }
}
