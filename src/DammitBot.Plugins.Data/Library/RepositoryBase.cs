using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DammitBot.Library
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
        where TEntity : class
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

        protected virtual IQueryable<TEntity> GetQueryable()
        {
            return _helper.GetQueryable<TEntity>();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Exposed Methods

        public virtual object Insert(TEntity entity)
        {
            return _helper.Insert(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _helper.Update(entity);
        }

        public virtual TEntity Find(int id)
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
