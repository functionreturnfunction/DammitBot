using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DammitBot.Scheduling.Metadata;
using DammitBot.Utilities;
using DammitBot.Utilities.AssemblyEnumerableExtensions;
using Quartz;
using StructureMap.TypeRules;

namespace DammitBot.Scheduling.Library
{
    public class JobService : IJobService
    {
        #region Private Members

        private readonly IAssemblyService _assemblyService;

        #endregion

        #region Constructors

        public JobService(IAssemblyService assemblyService)
        {
            _assemblyService = assemblyService;
        }

        #endregion

        #region Private Methods

        private static Action<SimpleScheduleBuilder> DetermineSchedule(MemberInfo jobType)
        {
            var interval = jobType.GetCustomAttribute<IntervalAttribute>();

            return x => interval.SetInterval(x).RepeatForever();
        }

        #endregion

        #region Exposed Methods

        public IEnumerable<Type> GetAllJobs()
        {
            foreach (
                var type in
                _assemblyService.GetAllAssemblies()
                    .GetTypes()
                    .Where(
                        t =>
                            !t.IsAbstract && typeof(IJob).IsAssignableFrom(t) && t.Name.EndsWith("Job") &&
                            t.HasAttribute<IntervalAttribute>()))
            {
                yield return type;
            }
        }

        public IJobDetail Build(Type jobType, string name, string group)
        {
            return JobBuilder.Create(jobType)
                .WithIdentity(name, group)
                .Build();
        }

        public ITrigger BuildTrigger(Type jobType, string name, string group)
        {
            return TriggerBuilder.Create()
                .WithIdentity(name, group)
                .StartNow()
                .WithSimpleSchedule(DetermineSchedule(jobType))
                .Build();
        }

        #endregion
    }
}