using JakubWiesniakLab3.DataAccess;
using JakubWiesniakLab3.DataAccess.Entities;
using JakubWiesniakLab3.Models;
using Microsoft.AspNetCore.Identity;

namespace JakubWiesniakLab3.Repositories.Users;

public class AccountRepository : IAccountRepository
{
    private readonly AppDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;

    public AccountRepository(AppDbContext context,
        IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public void RegisterUser(RegisterViewModel dto)
    {
        var newUser = new User()
        {
            UserName = dto.UserName,
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PasswordHash = string.Empty
        };
        var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);
        newUser.PasswordHash = hashedPassword;

        _context.Users.Add(newUser);
        _context.SaveChanges();
    }

    public User? GetUser(LoginViewModel dto)
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
}