using System;
using DammitBot.Library;

namespace DammitBot.Data.Models
{
    public class Message : IThingWithTimestamps
    {
        #region Properties

        public virtual int Id { get; set; }
        public virtual string Text { get; set; }
        public virtual string Protocol { get; set; }
        public virtual string Channel { get; set; }
        public virtual Nick From { get; set; }
        public virtual int? FromId { get; set; }

        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime? UpdatedAt { get; set; }

        #endregion
    }
}