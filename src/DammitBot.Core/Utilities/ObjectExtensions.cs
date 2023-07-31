using System;
using System.Linq;
using System.Reflection;

namespace DammitBot.Utilities
{
    public static class ObjectExtensions
    {
        public static bool TrySetDateTimeProperty(this object that, string propertyName, DateTime value)
        {
            var prop = that.GetType().GetProperties(BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.Instance).SingleOrDefault(p => p.Name == propertyName);

            if (prop == null)
            {
                return false;
            }

            prop.SetValue(that, value);
            return true;
        }
    }
}