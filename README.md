# DammitBot

Configurable, modular chatbot written in c#.

## Features

- Plugin system

Thats it.  As of this writing, all the core bot is capable of doing is connecting to IRC, setting its nick and joining a channel.

## Plugins

Plugins are implemented by implementing functionality that overlays/overwrites default functionality in the bot.  There are join points which allow plugins to add

### Commands

Commands are messages sent to the bot beginning with a name it has been configured to respond to.  The default name the bot will respond to is "bot" or "dammit bot".

#### Die

Tells the bot to stop running.

### Scheduling

Scheduling plugins use Quartz.Scheduler to run code in backround threads at set schedules.

#### Team City

The TeamCity plugin will connect to the web API of a JetBrains TeamCity server to notify a chat room as builds pass or fail.

## Libraries

DammitBot employs the following 3rd party libraries (mostly through nuget):

- [Castle.Core](https://www.nuget.org/packages/Castle.Core/)
- [ChatSharp](https://www.nuget.org/packages/ChatSharp/)
- [Common.Logging](https://www.nuget.org/packages/Common.Logging/)
- [Common.Logging.Core](https://www.nuget.org/packages/Common.Logging.Core/)
- [EasyHttp](https://www.nuget.org/packages/EasyHttp/)
- [JsonFX](https://www.nuget.org/packages/JsonFx/)
- [log4net](https://www.nuget.org/packages/log4net/)
- [Quartz](https://www.nuget.org/packages/Quartz/)
- [StructureMap](https://www.nuget.org/packages/StructureMap/)
- [TeamCitySharper](https://www.nuget.org/packages/TeamCitySharper/)
