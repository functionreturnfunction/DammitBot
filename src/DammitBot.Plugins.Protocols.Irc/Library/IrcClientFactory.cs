using System.Diagnostics.CodeAnalysis;
using DammitBot.Wrappers;

namespace DammitBot.Library;

/// <inheritdoc/>
[ExcludeFromCodeCoverage]
public class IrcClientFactory : IIrcClientFactory
{
    #region Private Members
    
    private readonly IInstantiationService _instantiationService;
    
    #endregion
    
    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="IrcClientFactory"/> class.
    /// </summary>
    public IrcClientFactory(IInstantiationService instantiationService)
    {
        _instantiationService = instantiationService;
    }
    
    #endregion

    #region Exposed Methods

    /// <inheritdoc />
    public IIrcClient Build()
    {
        return _instantiationService.GetInstance<IIrcClient>();
    }

    #endregion
}