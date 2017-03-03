using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DammitBot.Data.Library
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
    {
        #region Private Members

        protected readonly IDataCommandHelper _helper;

        #endregion

        #region Properties

        public Expression Expression => GetQueryable().Expression;

        public Type ElementType => typeof(TEntity);

        public IQueryProvider Provider => GetQueryable().Provider;

        #endregion

        #region Constructors

        protected RepositoryBase(IDataCommandHelper helper)
        {
            _helper = helper;
        }

        #endregion

        #region Private Methods

        private IQueryable<TEntity> GetQueryable()
        {
            return _helper.GetQueryable<TEntity>();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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

        public IEnumerator<TEntity> GetEnumerator()
        {
            return GetQueryable().GetEnumerator();
        }

        #endregion
    }
}