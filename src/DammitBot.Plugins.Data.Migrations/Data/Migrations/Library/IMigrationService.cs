using System.Collections.Generic;
using DammitBot.Abstract;

namespace DammitBot.Data.Migrations.Library;

/// <inheritdoc cref="ThingyServiceBase{TThingy}"/>
/// <remarks>
/// This implementation provides instances of <see cref="MigrationBase"/> implementations.
/// </remarks>
public interface IMigrationService
{
    /// <inheritdoc cref="ThingyServiceBase{TThingy}.Thingies"/>
    /// <inheritdoc cref="IMigrationService" path="remarks"/>
    IEnumerable<MigrationBase> Thingies { get; }
}