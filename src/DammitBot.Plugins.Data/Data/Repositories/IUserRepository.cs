﻿using DammitBot.Data.Models;
using DammitBot.Library;

namespace DammitBot.Data.Repositories;

/// <summary>
/// <see cref="IRepository{TEntity}"/> implementation responsible for <see cref="User"/> entities.
/// </summary>
public interface IUserRepository : IRepository<User>
{
    /// <summary>
    /// Find the <see cref="User"/> with the given <paramref name="username"/>. Returns null if not found.
    /// </summary>
    User? FindByUsername(string username);
}