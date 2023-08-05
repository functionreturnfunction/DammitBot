using System;
using System.Linq;
using System.Reflection;

namespace DammitBot.Utilities;

/// <summary>
/// Extensions to the <see cref="Object"/> class.
/// </summary>
public static class ObjectExtensions
{
    /// <summary>
    /// Attempt to set the <see cref="DateTime"/> with the specified <paramref name="propertyName"/> to
    /// the specified <paramref name="value"/>.  If the object has a property with that name which is of
    /// type <see cref="DateTime"/>, the property will be set and true will be returned.  If the object
    /// does not have a property with that name, or has a property of that name which is not of type
    /// <see cref="DateTime"/>, false is returned.
    /// </summary>
    public static bool TrySetDateTimeProperty(this object that, string propertyName, DateTime value)
    {
        var prop = that.GetType()
            .GetProperties(
                BindingFlags.Public |
                BindingFlags.SetProperty |
                BindingFlags.Instance)
            .SingleOrDefault(p => p.Name == propertyName);

        if (prop == null || !new[] { typeof(DateTime), typeof(DateTime?) }.Contains(prop.PropertyType))
        {
            return false;
        }

        prop.SetValue(that, value);
            
        return true;
    }
}