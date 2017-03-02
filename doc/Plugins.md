## Note:

All plugins depend on the DammitBot.Core library.  

## DammitBot.Plugins.Commands.dll

Base plugin implementing bot commands.

### Dependencies

- StructureMap

## DammitBot.Plugins.Commands.Die.dll

Plugin implementing the "die" command, which allows chat users to shut down the bot from the chat.

### Dependencies

- DammitBot.Plugins.Commands.dll

## DammitBot.Plugins.Commands.Help.dll

Plugin implmenting the "help" command, giving users a list of available commands and their usage, as well as an "unknown" command to refer users to usage instructions.

### Dependencies

- StructureMap
- DammitBot.Plugins.Commands.dll

## DammitBot.Plugins.MessageLogging.dll

Plugin implementing message logging.

- DammitBot.Plugins.Data.dll

## DammitBot.Plugins.Scheduling.dll

Base plugin implementing scheduling.

### Dependencies

- Common.Logging
- Common.Logging.Core
- Quartz
- StructureMap

## DammitBot.Plugins.Scheduling.TeamCity.dll

Plugin implementing TeamCity build notifications.

### Dependencies

- Common.Logging
- Common.Logging.Core
- log4net
- Quartz
- StructureMap
- TeamCitySharper
- DammitBot.Plugins.Scheduling.dll
