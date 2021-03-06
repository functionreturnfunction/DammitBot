﻿using System;
using System.Linq;

namespace DammitBot.Data.Library
{
    /// <summary>
    /// Service for simple CRUD access to data without having to consume repositories or units of work.
    /// This is most likely a crutch if you are making more than one call with it.
    /// </summary>
    public interface IPersistenceService : IDisposable
    {
        T Save<T>(T obj);
        T Find<T>(object id);
        IQueryable<T> Query<T>();
    }
}