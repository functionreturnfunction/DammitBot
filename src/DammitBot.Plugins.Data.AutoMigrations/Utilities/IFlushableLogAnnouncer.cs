using FluentMigrator.Runner;

namespace DammitBot.Utilities
{
    public interface IFlushableLogAnnouncer : IAnnouncer
    {
        void Flush();
    }
}