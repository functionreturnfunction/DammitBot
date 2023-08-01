using System;
using System.Collections.Generic;
using Quartz;

namespace DammitBot.Library
{
    public interface IJobService
    {
        IEnumerable<Type> GetAllJobs();
        IJobDetail Build(Type jobType, string name, string group);
        ITrigger BuildTrigger(Type jobType, string triggerName, string group);
    }
}