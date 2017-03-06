using System;
using DammitBot.Data.Library;

namespace DammitBot.Data.Models
{
    public class Reminder : IThingWithTimestamps
    {
        #region Properties

        public virtual int Id { get; set; }
        public virtual string Text { get; set; }
        public virtual DateTime? RemindAt { get; set; }
        public virtual DateTime? RemindedAt { get; set; }
        public virtual bool Sent { get; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime? UpdatedAt { get; set; }
        public virtual User From { get; set; }
        public virtual User To { get; set; }

        #endregion
    }
}
