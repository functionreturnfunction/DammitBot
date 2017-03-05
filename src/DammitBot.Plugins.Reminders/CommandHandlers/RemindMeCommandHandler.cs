using System;
using DammitBot.Data.Library;
using DammitBot.Events;
using DammitBot.Metadata;

namespace DammitBot.CommandHandlers
{
    [HandlesCommand("^remind me.*")]
    public class RemindMeCommandHandler : CommandHandlerBase
    {
        public const string REGEX = "$remind me (?:to|that) (.+) (?:at|on) (.+)^";

        #region Private Members

        private readonly IPersistenceService _persistenceService;

        #endregion

        #region Constructors

        public RemindMeCommandHandler(IBot bot, IPersistenceService persistenceService) : base(bot)
        {
            _persistenceService = persistenceService;
        }

        #endregion

        #region Exposed Methods

        public override void Handle(CommandEventArgs e)
        {
            
            throw new NotImplementedException();
        }

        #endregion
    }
}
