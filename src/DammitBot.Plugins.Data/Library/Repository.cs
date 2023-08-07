namespace DammitBot.Library;

public class Repository<TEntity> : RepositoryBase<TEntity>
    where TEntity : class
{
    #region Constructors

    public Repository(IDataCommandService commandService) : base(commandService) { }

    #endregion
}