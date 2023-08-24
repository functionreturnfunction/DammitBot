using System;
using DammitBot.Library;
using Lamar;
using Xunit;

namespace DammitBot.Data.Dapper.Repositories;

public abstract class DapperRepositoryTestBase<TEntity, TRepository>
    : InMemoryDatabaseUnitTestBase<TRepository>
    where TRepository : DapperRepositoryBase<TEntity>
    where TEntity : class, IEntityWithTimestamps
{
    #region Setup/Teardown
    
    protected override void ConfigureContainer(ServiceRegistry serviceRegistry)
    {
        base.ConfigureContainer(serviceRegistry);

        // ensure that uow extension methods use the generic repository instead of target so things can be
        // tested in isolation
        serviceRegistry.For<IRepository<TEntity>>().Use<Repository<TEntity>>();
    }
    
    #endregion

    #region Abstract Methods
    
    protected abstract TEntity CreateValidEntity();

    protected abstract void EnsureReferenceIds(IUnitOfWork uow, TEntity entity);

    protected abstract void MakeUpdateChange(TEntity entity);

    protected abstract void TestUpdateChange(TEntity entity);
    
    #endregion
    
    #region Private Methods

    protected TEntity InsertValidEntity()
    {
        var entity = CreateValidEntity();

        WithUnitOfWork(uow => {
            EnsureReferenceIds(uow, entity);
            entity.Id = Convert.ToInt32(uow.Insert(entity));
            
            uow.Commit();
        });

        return entity;
    }
    
    #endregion
    
    #region Find(id) tests

    [Fact]
    public void Test_Find_FindsEntityById()
    {
        var existing = InsertValidEntity();

        Assert.Equivalent(existing, _target.Find(existing.Id));
    }

    [Fact]
    public void Test_Find_ReturnsNull_WhenEntityNotFound()
    {
        Assert.Null(_target.Find(0));
    }
    
    #endregion
    
    #region Insert(entity) tests

    [Fact]
    public void Test_Insert_CreatesEntity()
    {
        var entity = CreateValidEntity();

        WithUnitOfWork(uow => {
            EnsureReferenceIds(uow, entity);
            
            uow.Commit();
        });

        entity.Id = Convert.ToInt32(_target.Insert(entity));

        Assert.Equivalent(entity, _target.Find(entity.Id));
    }

    [Fact]
    public void Test_Insert_SetsCreatedAt()
    {
        var entity = CreateValidEntity();

        WithUnitOfWork(uow => {
            EnsureReferenceIds(uow, entity);
            
            uow.Commit();
        });

        entity.Id = Convert.ToInt32(_target.Insert(entity));

        Assert.Equivalent(_now, _target.Find(entity.Id)!.CreatedAt);
    }
    
    #endregion
    
    #region Update(entity) tests
    
    // TODO: should not set updated at or call db when nothing changed
    
    [Fact]
    public void Test_Update_UpdatesEntity()
    {
        var existing = InsertValidEntity();

        MakeUpdateChange(existing);
        
        _target.Update(existing);

        TestUpdateChange(_target.Find(existing.Id));
    }
    
    [Fact]
    public void Test_Update_SetsUpdatedAt()
    {
        var existing = InsertValidEntity();

        _target.Update(existing);
        
        Assert.Equal(_now, _target.Find(existing.Id)!.UpdatedAt);
    }
    
    #endregion
}