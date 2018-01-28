using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DammitBot.Abstract;
using DammitBot.Events;

namespace DammitBot.CommandHandlers
{
    public class CompositeCommandHandlerTest : CompositeMessageHandlerTestBase<CompositeCommandHandler, ICommandHandler, CommandEventArgs>
    {
    }
}
