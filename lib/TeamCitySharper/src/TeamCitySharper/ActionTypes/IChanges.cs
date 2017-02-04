using System.Collections.Generic;
using TeamCitySharper.DomainEntities;

namespace TeamCitySharper.ActionTypes
{
    public interface IChanges
    {
        List<Change> All();
        Change ByChangeId(string id);
        Change LastChangeDetailByBuildConfigId(string buildConfigId);
        List<Change> ByBuildConfigId(string buildConfigId);
    }
}