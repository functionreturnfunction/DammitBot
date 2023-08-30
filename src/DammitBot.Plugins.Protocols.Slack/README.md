# DammitBot.Plugins.Protocols.Slack

This [DammitBot](../DammitBot.Core/README.md) protocol library plugin provides types and basic support for
connecting and messaging over the Slack protocol.  This library plugin cannot connect to Slack on its own,
and requires a second plugin with the actual implementation details, so as to stay agnostic about the 3rd
party library used to fill in those blanks and provide a uniform interface for handling Slack support.