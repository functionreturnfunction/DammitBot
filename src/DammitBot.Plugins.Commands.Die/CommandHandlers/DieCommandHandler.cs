using DammitBot.Events;
using DammitBot.Metadata;

namespace DammitBot.CommandHandlers
{
    [HandlesCommand("^die$")]
    public class DieCommandHandler : CommandHandlerBase
    {
        #region Constants

        public const string MESSAGE = "Alright fine.";

        #endregion

        #region Constructors

        public DieCommandHandler(IBot bot) : base(bot) {}

        #endregion

        #region Exposed Methods

        public override void Handle(CommandEventArgs e)
        {
            _bot.SayToAll(MESSAGE);
            _bot.Die();
        }

        #endregion
    }
}
