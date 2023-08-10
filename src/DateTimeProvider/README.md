# DateTimeProvider

This library provides functionality for injecting into the system a date/time value which it can consider
to be "the current date and time".  This is necessary because DateTime.Now is a static value which is
pulled directly from the machine clock, and thus code based on it is less testable and subject to an
external side-effect (the current date and time as the machine running the code understands it to be).