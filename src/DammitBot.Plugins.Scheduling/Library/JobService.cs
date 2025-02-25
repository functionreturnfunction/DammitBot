﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DammitBot.Metadata;
using DammitBot.Utilities;
using Quartz;

namespace DammitBot.Library;

/// <inheritdoc />
public class JobService : IJobService
{
    #region Private Members

    private readonly IAssemblyTypeService _assemblyTypeService;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="JobService"/> class.
    /// </summary>
    public JobService(IAssemblyTypeService assemblyTypeService)
    {
        _assemblyTypeService = assemblyTypeService;
    }

    #endregion

    #region Private Methods

    private static Action<ISimpleScheduleBuilder> DetermineSchedule(MemberInfo jobType)
    {
        var interval = jobType.GetCustomAttribute<IntervalAttribute>()!;

        return x => interval.SetInterval(x).RepeatForever();
    }

    #endregion

    #region Exposed Methods

    /// <inheritdoc />
    public IEnumerable<Type> GetAllJobs()
    {
        // TODO: try and pare this down with ThingyServiceBase<T> somehow
        foreach (
            var type in
            _assemblyTypeService.GetTypesFromAllAssemblies()
                .Where(
                    t =>
                        !t.IsAbstract && typeof(IJob).IsAssignableFrom(t) && t.Name.EndsWith("Job") &&
                        t.HasAttribute<IntervalAttribute>()))
        {
            yield return type;
        }
    }

    /// <inheritdoc />
    public IJobDetail Build(Type jobType, string name, string group)
    {
        return JobBuilder.Create(jobType)
            .WithIdentity(name, group)
            .Build();
    }

    /// <inheritdoc />
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