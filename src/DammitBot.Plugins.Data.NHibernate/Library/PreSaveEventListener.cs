using System;
using System.Reflection;
using DammitBot.Utilities;
using DammitBot.Utilities.TypeExtensions;
using NHibernate.Event;
using NHibernate.Persister.Entity;

namespace DammitBot.Data.NHibernate.Library
{
    public class PreSaveEventListener : IPreInsertEventListener, IPreUpdateEventListener
    {
        #region Private Members

        private readonly IDateTimeProvider _dateTimeProvider;

        #endregion

        #region Constructors

        public PreSaveEventListener(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        #endregion

        #region Private Methods

        private static void SetState(IEntityPersister persister, object[] state, string propertyName, object value)
        {
            var index = Array.IndexOf(persister.PropertyNames, propertyName);
            if (index == -1)
            {
                return;
            }
            state[index] = value;
        }

        private static void SetObject(object entity, string property, object value)
        {
            PropertyInfo toSet;
            if (entity.GetType().HasProperty(property, out toSet))
            {
                toSet.SetValue(entity, value);
            }
        }

        #endregion

        #region Exposed Methods

        public bool OnPreInsert(PreInsertEvent @event)
        {
            var now = _dateTimeProvider.GetCurrentTime();
            SetState(@event.Persister, @event.State, "CreatedAt", now);
            SetObject(@event.Entity, "CreatedAt", now);
            return false;
        }

        public bool OnPreUpdate(PreUpdateEvent @event)
        {
            var now = _dateTimeProvider.GetCurrentTime();
            SetState(@event.Persister, @event.State, "UpdatedAt", now);
            SetObject(@event.Entity, "UpdatedAt", now);
            return false;
        }

        #endregion
    }
}