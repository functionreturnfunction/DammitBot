# DammitBot.Core

This library provides the basic bot implementation, which does nothing on its own beyond loading any
available plugins/protocols and then running, as well as the interfaces/abstract types from which plugins
and protocols can be implemented (and plugged-in).  No other libraries are (or should be) referenced by
this one; plugins are loaded automatically by virtue of having their assembly in the same directory as
this one when the bot is launched.