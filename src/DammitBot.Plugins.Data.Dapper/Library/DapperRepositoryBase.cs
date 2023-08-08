using System.Collections.Generic;
using System.Data;
using System.Linq;
using DammitBot.Utilities;
using DateTimeProvider;

namespace DammitBot.Library;

/// <inheritdoc />
/// <remarks>
/// This implementation uses <see cref="Dapper"/> to do the actual work of sending data to and receiving
/// data from the database.
/// </remarks>
public abstract class DapperRepositoryBase<TEntity> : RepositoryBase<TEntity>
    where TEntity : class
{
    #region Private Members
    
    /// <summary>
    /// <see cref="IDbConnection"/> by which to query the database.
    /// </summary>
    protected readonly IDbConnection _connection;
    private readonly IDateTimeProvider _dateTimeProvider;

    #endregion
    
    #region Properties
    
    /// <summary>
    /// Base string query used to look up instances of <typeparamref name="TEntity"/> from the database.
    /// This should include any necessary joins for loading child objects.
    /// </summary>
    protected abstract string BaseQuery { get; }
    
    #endregion
    
    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="DapperRepositoryBase{TEntity}"/> class.
    /// </summary>
    protected DapperRepositoryBase(
        IDataCommandService commandService,
        IDbConnection connection,
        IDateTimeProvider dateTimeProvider)
        : base(commandService)
    {
        _connection = connection;
        _dateTimeProvider = dateTimeProvider;
    }
    
    #endregion
    
    #region Abstract Methods

    /// <summary>
    /// <see cref="Dapper"/> doesn't automatically link child/joined objects, so that should be handled by
    /// inheriting classes in overrides of this method.
    /// </summary>
    protected abstract void FixReferences(TEntity entity);

    /// <summary>
    /// Implement the application of sql queries to pull data from persistence by overriding this method
    /// in inheriting classes.  Any joined/child objects should be included, in the order that they're
    /// joined.
    /// </summary>
    protected abstract IEnumerable<TEntity> DoQuery(string sql, object? param = null);
    
    #endregion
    
    #region Private Methods
    
    private void SetTimestamp(TEntity entity, string name)
    {
        entity.TrySetDateTimeProperty(name, _dateTimeProvider.GetCurrentTime());
    }
    
    #endregion
    
    #region Exposed Methods

    /// <inheritdoc />
    public override TEntity? Find(int id)
    {
        return DoQuery(BaseQuery + " where this.Id = @id", new {id}).SingleOrDefault();
    }

    /// <inheritdoc />
    public override object Insert(TEntity entity)
    {
        FixReferences(entity);
        SetTimestamp(entity, "CreatedAt");
        return base.Insert(entity);
    }

    /// <inheritdoc />
    public override void Update(TEntity entity)
    {
        FixReferences(entity);
        SetTimestamp(entity, "UpdatedAt");
        base.Update(entity);
    }
    
    #endregion
}