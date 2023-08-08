using DammitBot.Data.Models;
using DammitBot.Library;

namespace DammitBot.Data.Repositories;

public interface INickRepository : IRepository<Nick>
{
    public Nick? FindByNickname(string nickname);
}