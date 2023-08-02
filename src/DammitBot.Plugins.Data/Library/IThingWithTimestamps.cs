using System;

namespace DammitBot.Library;

public interface IThingWithTimestamps
{
    DateTime CreatedAt { get; set; }
    DateTime? UpdatedAt { get; set; }
}