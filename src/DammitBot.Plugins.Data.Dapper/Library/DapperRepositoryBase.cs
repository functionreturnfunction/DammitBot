using System.Collections.Generic;
using System.Data;
using System.Linq;
using DammitBot.Data.Library;
using Dapper;

namespace DammitBot.Data.Library
{
    public abstract class DapperRepositoryBase<TEntity> : RepositoryBase<TEntity>
        where TEntity : class
    {
        protected readonly IDbConnection _connection;

        protected abstract string BaseQuery { get; }

        protected abstract IEnumerable<TEntity> DoQuery(string sql);

        public DapperRepositoryBase(IDataCommandHelper helper, IDbConnection connection) : base(helper)
        {
            _connection = connection;
        }

        protected override IQueryable<TEntity> GetQueryable()
        {
            return DoQuery(BaseQuery + " order by this.id").AsQueryable();
        }

        public override TEntity Find(int id)
        {
            return DoQuery(BaseQuery + $" where this.Id = {id}").SingleOrDefault();
        }
    }
}
