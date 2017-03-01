﻿using System;

namespace DammitBot.Data.Models
{
    public class Message
    {
        #region Properties

        public virtual int Id { get; set; }
        public virtual string Text { get; set; }
        public virtual Nick From { get; set; }

        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime? UpdatedAt { get; set; }

        #endregion
    }
}