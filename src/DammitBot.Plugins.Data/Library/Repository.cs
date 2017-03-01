using System;
using System.Linq;
using System.Linq.Expressions;

namespace DammitBot.Data.Library
{
    public class Repository<TEntity> : RepositoryBase<TEntity>
    {
        #region Private Members

        private readonly IDataCommandHelper _helper;

        #endregion

        #region Constructors

        public Repository(IDataCommandHelper helper)
        {
            _helper = helper;
        }

        #endregion

        #region Exposed Methods

        public override TEntity Save(TEntity entity)
        {
            _helper.Save(entity);
            return entity;
        }

        public override TEntity Find(object id)
        {
            return _helper.Load<TEntity>(id);
        }

        public override IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> fn)
        {
            return _helper.Where(fn);
        }

        #endregion
    }
}
