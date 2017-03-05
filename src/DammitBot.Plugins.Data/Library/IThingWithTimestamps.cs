using System;

namespace DammitBot.Data.Library
{
    public interface IThingWithTimestamps
    {
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
    }
}