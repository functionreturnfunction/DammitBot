using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DammitBot.Abstract;
using DammitBot.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DammitBot.CommandHandlers
{
    [TestClass]
    public class CompositeCommandHandlerTest :CompositeMessageHandlerTestBase<CompositeCommandHandler, ICommandHandler, CommandEventArgs>
    {
    }
}
