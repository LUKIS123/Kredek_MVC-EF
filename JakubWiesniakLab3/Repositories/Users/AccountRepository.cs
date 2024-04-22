using JakubWiesniakLab3.DataAccess.Entities;
using JakubWiesniakLab3.Models;

namespace JakubWiesniakLab3.Repositories.Users;

public class AccountRepository : IAccountRepository
{
    public void RegisterUser(RegisterViewModel dto)
    {
        throw new NotImplementedException();
    }

    public User? LoginUser(LoginViewModel dto)
    {
        throw new NotImplementedException();
    }

    public User? GetUser(string username)
    {
        throw new NotImplementedException();
    }
}