using System;
using System.Linq;
using System.Linq.Expressions;

namespace DammitBot.Data.Library
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
    {
        #region Private Members

        protected readonly IDataCommandHelper _helper;

        #endregion

        #region Constructors

        protected RepositoryBase(IDataCommandHelper helper)
        {
            _helper = helper;
        }

        #endregion

        #region Exposed Methods

        public virtual TEntity Save(TEntity entity)
        {
            _helper.Save(entity);
            return entity;
        }

        public virtual TEntity Find(object id)
        {
            return _helper.Load<TEntity>(id);
        }

        public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> fn)
        {
            return _helper.Where(fn);
        }

        #endregion
    }
}