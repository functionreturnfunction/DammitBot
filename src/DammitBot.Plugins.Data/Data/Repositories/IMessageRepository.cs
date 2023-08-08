using DammitBot.Data.Models;
using DammitBot.Library;

namespace DammitBot.Data.Repositories;

/// <summary>
/// <see cref="IRepository{TEntity}"/> implementation responsible for <see cref="Message"/> entities.
/// </summary>
public interface IMessageRepository : IRepository<Message> { }