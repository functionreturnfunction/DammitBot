# DammitBot.Plugins.Protocols.Console

This [DammitBot](../DammitBot.Core/README.md) protocol library plugin provides types and basic support for
connecting and messaging over the Irc protocol.  This library plugin cannot connect to Irc on its own, and
requires a second plugin with the actual implementation details, so as to stay agnostic about the 3rd
party library used to fill in those blanks and provide a uniform interface for handling Irc support.