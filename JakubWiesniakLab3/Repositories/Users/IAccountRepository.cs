using JakubWiesniakLab3.DataAccess.Entities;
using JakubWiesniakLab3.Models;

namespace JakubWiesniakLab3.Repositories.Users
{
    public interface IAccountRepository
    {
        void RegisterUser(RegisterViewModel dto);
        User? GetUser(LoginViewModel dto);
    }
}