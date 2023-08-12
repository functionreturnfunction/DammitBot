using System.Collections.Generic;
using DammitBot.Abstract;

namespace DammitBot.Library;

/// <inheritdoc cref="ThingyServiceBase{MigrationBase}"/>
/// <remarks>
/// This implementation provides instances of <see cref="MigrationBase"/> implementations.
/// </remarks>
public interface IMigrationService
{
    /// <inheritdoc cref="ThingyServiceBase{MigrationBase}.Thingies"/>
    /// <inheritdoc cref="IMigrationService" path="remarks"/>
    IEnumerable<MigrationBase> Thingies { get; }
}