# DammitBot.Plugins.Data

This [DammitBot](../DammitBot.Core/README.md) plugin provides the most basic support for handling
persistent data in the context of the bot.  A basic domain model is presented with classes for
[Message](Data/Models/Message.cs), [Nick](Data/Models/Nick.cs), and [User](Data/Models/User.cs), as well
as shell implementations of the UnitOfWork->Repository pattern.  This cannot operate on data on its own,
and requires further plugged-in functionality to actually persist and query data.