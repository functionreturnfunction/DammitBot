using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DammitBot.Library;

/// <inheritdoc />
public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    #region Private Members

    private readonly IDataCommandService _commandService;

    #endregion

    #region Properties

    /// <inheritdoc cref="IQueryable{TEntity}.Expression"/>
    public Expression Expression => GetQueryable().Expression;

    /// <inheritdoc cref="IQueryable{TEntity}.ElementType"/>
    public Type ElementType => typeof(TEntity);

    /// <inheritdoc cref="IQueryable{TEntity}.Provider"/>
    public IQueryProvider Provider => GetQueryable().Provider;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="RepositoryBase{TEntity}"/> class.
    /// </summary>
    protected RepositoryBase(IDataCommandService commandService)
    {
        _commandService = commandService;
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Retrieve an <see cref="IQueryable{TEntity}"/> instance to query against.
    /// </summary>
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

    /// <inheritdoc cref="IRepository{TEntity}.Insert"/>
    public virtual object Insert(TEntity entity)
    {
        return _commandService.Insert(entity);
    }

    /// <inheritdoc cref="IRepository{TEntity}.Update"/>
    public virtual void Update(TEntity entity)
    {
        _commandService.Update(entity);
    }

    /// <inheritdoc cref="IRepository{TEntity}.Find"/>
    public virtual TEntity Find(int id)
    {
        return _commandService.Load<TEntity>(id);
    }

    /// <inheritdoc cref="IEnumerable{TEntity}.GetEnumerator"/>
    public IEnumerator<TEntity> GetEnumerator()
    {
        return GetQueryable().GetEnumerator();
    }

    #endregion
}