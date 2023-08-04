using System.Diagnostics.CodeAnalysis;
using DammitBot.Wrappers;

namespace DammitBot.Library;

[ExcludeFromCodeCoverage]
public class IrcClientFactory : IIrcClientFactory
{
    private readonly IInstantiationService _instantiationService;

    public IrcClientFactory(IInstantiationService instantiationService)
    {
        _instantiationService = instantiationService;
    }

    #region Exposed Methods

    public IIrcClient Build()
    {
        return _instantiationService.GetInstance<IIrcClient>();
    }

    #endregion
}