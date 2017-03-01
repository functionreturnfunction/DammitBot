﻿using System;
using System.Linq;
using System.Linq.Expressions;
using DammitBot.Data.Library;
using NHibernate;
using NHibernate.Linq;

namespace DammitBot.Data.NHibernate.Library
{
    public class DataCommandHelper : IDataCommandHelper
    {
        #region Private Members

        private readonly ISession _session;

        #endregion

        #region Constructors

        public DataCommandHelper(ISession session)
        {
            _session = session;
        }

        #endregion

        #region Exposed Methods

        public void Save(object entity)
        {
            _session.Save(entity);
        }

        public T Load<T>(object id)
        {
            return _session.Load<T>(id);
        }

        public IQueryable<T> Where<T>(Expression<Func<T, bool>> fn)
        {
            return _session.Query<T>().Where(fn);
        }

        #endregion
    }
}
