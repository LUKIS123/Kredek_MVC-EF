using JakubWiesniakLab3.DataAccess.Entities;
using JakubWiesniakLab3.Models;

namespace JakubWiesniakLab3.Repositories.Users
{
    public interface IAccountRepository
    {
        void RegisterUser(RegisterViewModel dto);
        User? LoginUser(LoginViewModel dto);
        User? GetUser(string username);
    }
}