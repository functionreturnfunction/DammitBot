using System.Collections.Generic;

namespace TeamCitySharper.DomainEntities
{
    public class BuildTriggers
    {
        public override string ToString()
        {
            return "triggers";
        }

        public List<BuildTrigger> Trigger { get; set; }
    }
}