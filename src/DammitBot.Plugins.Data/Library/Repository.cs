namespace DammitBot.Data.Library
{
    public class Repository<TEntity> : RepositoryBase<TEntity>
    {
        #region Constructors

        public Repository(IDataCommandHelper helper) : base(helper) { }

        #endregion
    }
}
