using System;
using DammitBot.Library;

namespace DammitBot.Data.Models;

public class User : IThingWithTimestamps
{
    #region Properties

    public virtual int Id { get; set; }
    public virtual string Username { get; set; }

    public virtual DateTime CreatedAt { get; set; }
    public virtual DateTime? UpdatedAt { get; set; }

    #endregion
}