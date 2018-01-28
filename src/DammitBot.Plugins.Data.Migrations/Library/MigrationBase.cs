using System.Data;
using DammitBot.Data.Library;

namespace DammitBot.Data.Migrations.Library
{
    public abstract class MigrationBase
    {
        public abstract int Id { get; }

        public abstract void Up(IDisposableUnitOfWork uow);

        public abstract void Down(IDisposableUnitOfWork uow);

        public virtual void Seed(IDisposableUnitOfWork uow) {}
    }
}