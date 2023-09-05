![status](https://mangler/gitlab/jason/dammit_bot/badges/master/pipeline.svg?ignore_skipped=true)![coverage](https://mangler/gitlab/jason/dammit_bot/badges/master/coverage.svg)

# DammitBot

Configurable, modular chatbot written in c#.

# Features

- Plugin system
- (optional) Message logging
- (optional) Time-based reminders
- (optional) Database with (optional) migrations and an (optional) auto-runner
- (optional) Bot commands

# [Plugins](doc/Plugins.md)

Plugins are implemented by implementing functionality that overlays/overwrites default functionality in the bot.  There are join points which allow plugins to add or override functionality via IoC configuration, and initialize plugins/protocols
at application start.

## Commands

Commands are messages sent to the bot beginning with a name it has been configured to respond to.  The default name the bot will respond to is "bot" or "dammit bot".

### Die

Tells the bot to stop running.

### Help

Provides usage information on available commands.

### Remind

Set reminders

## Scheduling

Scheduling plugins use Quartz.Scheduler to run code in backround threads at set schedules.

## Protocols

The core bot has no functionality to connect to any chat protocols, rather this functionality is provided by plugins.

### Irc

Irc is one of the protocols for which there is a plugin.

### Slack

Slack is another of the protocols which have been implemented via plugins.

### Console

Console support is implemented as a protocol plugin, messages can be sent to and received from the bot directly
via the command-line where it's been launched.

# Libraries

DammitBot employs the following 3rd party libraries:

## Runtime
- Chronic.Core
 - Dapper
 - Dapper.FluentMap
 - Dommel
 - IrcDotNet
 - Lamar
 - Lamar.Microsoft.DependencyInjection
 - Microsoft.Data.Sqlite.Core
 - Microsoft.Extensions.Configuration
 - Microsoft.Extensions.Configuration.Binder
 - Microsoft.Extensions.Configuration.Json
 - Microsoft.Extensions.FileSystemGlobbing
 - Microsoft.Extensions.Hosting
 - Microsoft.Extensions.Logging
 - Microsoft.Extensions.Options
 - Microsoft.Extensions.Options.ConfigurationExtensions
 - Microsoft.Extensions.Options.DataAnnotations
 - Quartz
 - SQLitePCLRaw.bundle_e_sqlite3
 - SlackNet
 - System.Configuration.ConfigurationManager

## Test
 - Bogus
 - coverlet.collector
 - JunitXml.TestLogger
 - Microsoft.NET.Test.Sdk
 - Moq
 - Moq.Sequences
 - xunit
 - xunit.runner.visualstudio
