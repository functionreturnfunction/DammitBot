using DammitBot.Library;

namespace DammitBot.Data.Migrations.Library;

public abstract class MigrationBase
{
    public abstract int Id { get; }

    public abstract void Up(IUnitOfWork uow);

    public abstract void Down(IUnitOfWork uow);

    public virtual void Seed(IUnitOfWork uow) {}
}