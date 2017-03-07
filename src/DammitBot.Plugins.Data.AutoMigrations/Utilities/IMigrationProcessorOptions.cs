using System;

namespace DammitBot.Utilities
{
    public interface IMigrationProcessorOptions : FluentMigrator.IMigrationProcessorOptions
    {
        #region Abstract Properties

        Type AnnouncerType { get; }
        Type FactoryType { get; }

        #endregion
    }
}