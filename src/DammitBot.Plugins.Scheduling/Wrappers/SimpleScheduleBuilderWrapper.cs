using System.Diagnostics.CodeAnalysis;
using DammitBot.Library;
using Quartz;

namespace DammitBot.Wrappers;

/// <inheritdoc />
[ExcludeFromCodeCoverage]
public class SimpleScheduleBuilderWrapper : ISimpleScheduleBuilder
{
    #region Private Members
    
    private readonly SimpleScheduleBuilder _innerBuilder;
    
    #endregion
    
    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="SimpleScheduleBuilderWrapper"/> class.
    /// </summary>
    public SimpleScheduleBuilderWrapper(SimpleScheduleBuilder innerBuilder)
    {
        _innerBuilder = innerBuilder;
    }
    
    #endregion
    
    #region Exposed Methods

    /// <inheritdoc />
    public ISimpleScheduleBuilder WithIntervalInHours(int interval)
    {
        return new SimpleScheduleBuilderWrapper(_innerBuilder.WithIntervalInHours(interval));
    }

    /// <inheritdoc />
    public ISimpleScheduleBuilder WithIntervalInSeconds(int interval)
    {
        return new SimpleScheduleBuilderWrapper(_innerBuilder.WithIntervalInSeconds(interval));
    }

    /// <inheritdoc />
    public ISimpleScheduleBuilder WithIntervalInMinutes(int interval)
    {
        return new SimpleScheduleBuilderWrapper(_innerBuilder.WithIntervalInMinutes(interval));
    }

    /// <inheritdoc />
    public ISimpleScheduleBuilder RepeatForever()
    {
        return new SimpleScheduleBuilderWrapper(_innerBuilder.RepeatForever());
    }
    
    #endregion
}