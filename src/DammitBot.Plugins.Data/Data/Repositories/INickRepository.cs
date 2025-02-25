﻿using DammitBot.Data.Models;
using DammitBot.Library;

namespace DammitBot.Data.Repositories;

/// <inheritdoc />
public interface INickRepository : IRepository<Nick>
{
    /// <summary>
    /// Find the <see cref="Nick"/> with the given <paramref name="nickname"/> and
    /// <paramref name="protocol"/>. Returns null if not found.
    /// </summary>
    public Nick? FindByNicknameAndProtocol(string nickname, string protocol);
}