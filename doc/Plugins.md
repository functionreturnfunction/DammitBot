## Note:

All plugins depend on the DammitBot.Core library.

## DammitBot.Plugins.Commands.dll

Base plugin implementing bot commands.

### Dependencies

- Lamar
- DammitBot.Plugins.Data.dll

## DammitBot.Plugins.Commands.Die.dll

Plugin implementing the "die" command, which allows chat users to shut down the bot from the chat.

### Dependencies

- DammitBot.Plugins.Commands.dll

## DammitBot.Plugins.Commands.Help.dll

Plugin implmenting the "help" command, giving users a list of available commands and their usage, as well as an "unknown" command to refer users to usage instructions.

### Dependencies

- Lamar
- DammitBot.Plugins.Commands.dll

## DammitBot.Plugins.Data.dll

Plugin implementing ORM and database agnostic data implementation.  No persistence functionality is defined, that must be provided by a further plugin.  Some pattern implementations such as Unit of Work/Repository are provided for convenience.

### Dependencies

- Lamar

## DammitBot.Plugins.Data.Migrations.dll

Plugin providing database migrations.

### Dependencies

- FluentMigrator

## DammitBot.Plugins.NHibernate.dll

Plugin providing ORM and Database support for NHibernate and MS Sql Server (so far).

### Dependencies

- FluentNHibernate
- Iesi.Collections
- Inflector
- NHibernate
- Lamar
- DammitBot.Plugins.Data.dll
- DateTimeStringParser.dll

## DammitBot.Plugins.MessageLogging.dll

Plugin implementing message logging.

### Dependencies

- DammitBot.Plugins.Data.dll

## DammitBot.Plugins.Protocols.Irc.dll

Plugin providing Irc protocol support.

### Dependencies

- ChatSharp
- log4net

## DammitBot.Plugins.Reminders.dll

Plugin providing reminders.

### Dependencies

- Common.Logging
- Common.Logging.Core
- FluentNHibernate
- Iesi.Collections
- log4net
- NHibernate
- Quartz
- DammitBot.Plugins.Commands.dll
- DammitBot.Plugins.Data.dll
- DammitBot.Plugins.Data.NHibernate.dll
- DammitBot.Plugins.Scheduling.dll
- DateTimeStringParser.dll

## DammitBot.Plugins.Reminders.Migrations.dll

Plugin providing database migrations for the reminders plugin.

### Dependencies

- FluentMigrator
- DammitBot.Plugins.Data.Migrations.dll

## DammitBot.Plugins.Scheduling.dll

Base plugin implementing scheduling.

### Dependencies

- Common.Logging
- Common.Logging.Core
- Quartz
- Lamar

## DammitBot.Plugins.TeamCity.dll

Plugin implementing TeamCity build notifications.

### Dependencies

- Common.Logging
- Common.Logging.Core
- log4net
- Quartz
- Lamar
- TeamCitySharper
- DammitBot.Plugins.Scheduling.dll
