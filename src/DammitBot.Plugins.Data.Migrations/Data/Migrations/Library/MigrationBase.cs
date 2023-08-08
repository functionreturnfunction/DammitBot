using DammitBot.Library;

namespace DammitBot.Data.Migrations.Library;

/// <summary>
/// Base class for database schema migrations and seeds.
/// </summary>
public abstract class MigrationBase
{
    #region Abstract Properties
    
    /// <summary>
    /// Used to keep track of what migrations have ran, and the order in which they should be run. Should
    /// be kept sequential.
    /// </summary>
    public abstract int VersionNumber { get; }
    
    #endregion
    
    #region Abstract Methods

    /// <summary>
    /// Make any schema changes necessary when the migration is applied. 
    /// </summary>
    public abstract void Up(IUnitOfWork uow);

    /// <summary>
    /// Make any schema changes necessary to undo what was done in <see cref="Up"/> when the migration is
    /// rolled back.
    /// </summary>
    public abstract void Down(IUnitOfWork uow);
    
    #endregion
    
    #region Exposed Methods

    /// <summary>
    /// Insert any necessary data into the database. This is run after <see cref="Up"/>.
    /// </summary>
    public virtual void Seed(IUnitOfWork uow) {}
    
    #endregion
}