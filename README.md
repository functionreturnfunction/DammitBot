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

Plugins are implemented by implementing functionality that overlays/overwrites default functionality in the bot.  There are join points which allow plugins to add

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

### Console

Console support is implemented as a protocol plugin, messages can be sent to and received from the bot directly
via the command-line where it's been launched.

# Libraries

DammitBot employs the following 3rd party libraries (mostly through nuget):

TODO!
