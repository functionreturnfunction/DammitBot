using System.Collections.Generic;

namespace DammitBot.Data.Migrations.Library
{
    public interface IMigrationService
    {
        IEnumerable<MigrationBase> Thingies { get; }
    }
}