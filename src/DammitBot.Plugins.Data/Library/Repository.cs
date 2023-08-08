namespace DammitBot.Library;

/// <inheritdoc />
/// <remarks>This implementation is meant to be instantiated and used directly.</remarks>
public sealed class Repository<TEntity> : RepositoryBase<TEntity>
    where TEntity : class
{
    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="Repository{TEntity}"/> class.
    /// </summary>
    public Repository(IDataCommandService commandService) : base(commandService) { }

    #endregion
}