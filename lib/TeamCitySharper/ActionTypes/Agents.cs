using System.Collections.Generic;
using TeamCitySharper.Connection;
using TeamCitySharper.DomainEntities;

namespace TeamCitySharper.ActionTypes
{
    internal class Agents : IAgents
    {
        private readonly TeamCityCaller _caller;

        internal Agents(TeamCityCaller caller)
        {
            _caller = caller;
        }

        public List<Agent> All()
        {
            var agentWrapper = _caller.Get<AgentWrapper>("/app/rest/agents");

            return agentWrapper.Agent;
        }
    }
}