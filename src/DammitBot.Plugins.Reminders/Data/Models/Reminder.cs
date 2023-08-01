using System;
using DammitBot.Library;
using DammitBot.Models;

namespace DammitBot.Data.Models
{
    public class Reminder : IThingWithTimestamps
    {
        #region Properties

        public virtual int Id { get; set; }
        public virtual string Text { get; set; }
        public virtual DateTime? RemindAt { get; set; }
        public virtual DateTime? RemindedAt { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime? UpdatedAt { get; set; }
        public virtual User From { get; set; }
        public virtual int? FromId { get; set; }
        public virtual User To { get; set; }
        public virtual int? ToId { get; set; }

        #endregion
    }
}
