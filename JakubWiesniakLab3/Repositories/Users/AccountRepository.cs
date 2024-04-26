using JakubWiesniakLab3.DataAccess;
using JakubWiesniakLab3.DataAccess.Entities;
using JakubWiesniakLab3.Models;
using Microsoft.AspNetCore.Identity;

namespace JakubWiesniakLab3.Repositories.Users;

public class AccountRepository : IAccountRepository
{
    private readonly AppDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;

    public AccountRepository(AppDbContext context, IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public void RegisterUser(RegisterViewModel dto)
    {
        var user = new User
        {
            Email = dto.Email,
            UserName = dto.UserName,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PasswordHash = string.Empty
        };

        var hashedPassword = _passwordHasher.HashPassword(user, dto.Password);
        user.PasswordHash = hashedPassword;

        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public User? LoginUser(LoginViewModel dto)
    {
        var user = _context.Users
            .FirstOrDefault(u => u.UserName == dto.UserName);

        if (user is null)
        {
            return null;
        }

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
        if (result != PasswordVerificationResult.Success)
        {
            return null;
        }

        return user;
    }

    public User? GetUser(string username)
    {
        return _context.Users
            .FirstOrDefault(u => u.UserName == username);
    }
}