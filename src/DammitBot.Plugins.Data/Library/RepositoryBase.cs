using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DammitBot.Library;

public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    #region Private Members

    protected readonly IDataCommandService _commandService;

    #endregion

    #region Properties

    public Expression Expression => GetQueryable().Expression;

    public Type ElementType => typeof(TEntity);

    public IQueryProvider Provider => GetQueryable().Provider;

    #endregion

    #region Constructors

    protected RepositoryBase(IDataCommandService commandService)
    {
        _commandService = commandService;
    }

    #endregion

    #region Private Methods

    protected virtual IQueryable<TEntity> GetQueryable()
    {
        return _commandService.GetQueryable<TEntity>();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    #endregion

    #region Exposed Methods

    public virtual object Insert(TEntity entity)
    {
        return _commandService.Insert(entity);
    }

    public virtual void Update(TEntity entity)
    {
        _commandService.Update(entity);
    }

    public virtual TEntity Find(int id)
    {
        return _commandService.Load<TEntity>(id);
    }

    public IEnumerator<TEntity> GetEnumerator()
    {
        return GetQueryable().GetEnumerator();
    }

    #endregion
}