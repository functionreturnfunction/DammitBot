using DammitBot.Data.Models;
using DammitBot.Library;

namespace DammitBot.Data.Repositories;

public interface IUserRepository : IRepository<User>
{
    User? FindByUsername(string username);
}