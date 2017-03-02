# DammitBot

Configurable, modular chatbot written in c#.

# Features

- Plugin system
- TeamCity build notifications
- Message logging

# [Plugins](doc/Plugins.md)

Plugins are implemented by implementing functionality that overlays/overwrites default functionality in the bot.  There are join points which allow plugins to add

## Commands

Commands are messages sent to the bot beginning with a name it has been configured to respond to.  The default name the bot will respond to is "bot" or "dammit bot".

### Die

Tells the bot to stop running.

## Scheduling

Scheduling plugins use Quartz.Scheduler to run code in backround threads at set schedules.

### Team City

The TeamCity plugin will connect to the web API of a JetBrains TeamCity server to notify a chat room as builds pass or fail.

## Protocols

The core bot has no functionality to connect to any chat protocols, rather this functionality is provided by plugins.

### Irc

Irc is one of the protocols for which there is a plugin.  It is currently the only such protocol.

# Libraries

DammitBot employs the following 3rd party libraries (mostly through nuget):

- [Castle.Core](https://www.nuget.org/packages/Castle.Core/)
- [ChatSharp](https://www.nuget.org/packages/ChatSharp/)
- [Common.Logging](https://www.nuget.org/packages/Common.Logging/)
- [Common.Logging.Core](https://www.nuget.org/packages/Common.Logging.Core/)
- [EasyHttp](https://www.nuget.org/packages/EasyHttp/)
- [FluentMigrator](https://www.nuget.org/packages/FluentMigrator/)
- [FluentNHibernate](https://www.nuget.org/packages/FluentNHibernate/)
- [Iesi.Collections](https://www.nuget.org/packages/Iesi.Collections/)
- [Inflector](https://www.nuget.org/packages/Inflector/)
- [JsonFX](https://www.nuget.org/packages/JsonFx/)
- [log4net](https://www.nuget.org/packages/log4net/)
- [Moq](https://www.nuget.org/packages/Moq/)
- [NHibernate](https://www.nuget.org/packages/NHibernate/)
- [Quartz](https://www.nuget.org/packages/Quartz/)
- [StructureMap](https://www.nuget.org/packages/StructureMap/)
- [TeamCitySharper](https://github.com/functionreturnfunction/TeamCitySharper)
