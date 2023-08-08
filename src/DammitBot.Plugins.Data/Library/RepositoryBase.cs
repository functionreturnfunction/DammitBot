namespace DammitBot.Library;

/// <inheritdoc />
public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    #region Private Members

    private readonly IDataCommandService _commandService;

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
    public virtual TEntity? Find(int id)
    {
        return _commandService.Load<TEntity>(id);
    }

    #endregion
}