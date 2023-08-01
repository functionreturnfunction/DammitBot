using System.Collections.Generic;
using System.Data;
using System.Linq;
using DammitBot.Library;
using DammitBot.Utilities;
using DateTimeStringParser;

namespace DammitBot.Data.Dapper.Library
{
    public abstract class DapperRepositoryBase<TEntity> : RepositoryBase<TEntity>
        where TEntity : class
    {
        protected readonly IDbConnection _connection;
        protected readonly IDateTimeProvider _dateTimeProvider;

        protected abstract string BaseQuery { get; }

        protected abstract IEnumerable<TEntity> DoQuery(string sql);

        public DapperRepositoryBase(IDataCommandHelper helper, IDbConnection connection, IDateTimeProvider dateTimeProvider) : base(helper)
        {
            _connection = connection;
            _dateTimeProvider = dateTimeProvider;
        }

        protected abstract void FixReferences(TEntity entity);

        protected override IQueryable<TEntity> GetQueryable()
        {
            return DoQuery(BaseQuery + " order by this.id").AsQueryable();
        }

        protected virtual void SetTimestamp(TEntity entity, string name)
        {
            entity.TrySetDateTimeProperty(name, _dateTimeProvider.GetCurrentTime());
        }

        public override TEntity Find(int id)
        {
            return DoQuery(BaseQuery + $" where this.Id = {id}").SingleOrDefault();
        }

        public override object Insert(TEntity entity)
        {
            FixReferences(entity);
            SetTimestamp(entity, "CreatedAt");
            return base.Insert(entity);
        }

        public override void Update(TEntity entity)
        {
            FixReferences(entity);
            SetTimestamp(entity, "UpdatedAt");
            base.Update(entity);
        }
    }
}
