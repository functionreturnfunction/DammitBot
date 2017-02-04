using System.Collections.Generic;
using TeamCitySharper.DomainEntities;

namespace TeamCitySharper.ActionTypes
{
    public interface IAgents
    {
        List<Agent> All();
    }
}