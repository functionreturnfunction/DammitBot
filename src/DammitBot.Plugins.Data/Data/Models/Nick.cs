using System;
using DammitBot.Library;

namespace DammitBot.Data.Models
{
    public class Nick : IThingWithTimestamps
    {
        #region Properties

        public virtual int Id { get; set; }
        public virtual string Nickname { get; set; }
        public virtual User User { get; set; }
        public virtual int? UserId { get; set; }

        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime? UpdatedAt { get; set; }

        #endregion
    }
}