using DammitBot.Data.Models;
using DammitBot.Library;

namespace DammitBot.Data.Repositories;

/// <summary>
/// <see cref="IRepository{TEntity}"/> implementation responsible for <see cref="Nick"/> entities.
/// </summary>
public interface INickRepository : IRepository<Nick>
{
    /// <summary>
    /// Find the <see cref="Nick"/> with the given <paramref name="nickname"/>. Returns null if not found.
    /// </summary>
    public Nick? FindByNickname(string nickname);
}