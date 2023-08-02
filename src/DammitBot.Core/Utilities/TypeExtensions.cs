using System;
using System.Linq;
using System.Reflection;

namespace DammitBot.Utilities;

public static class TypeExtensions
{
    #region Exposed Methods

    public static bool HasProperty(this Type that, string propertyName, out PropertyInfo property)
    {
        return that.HasProperty<object>(propertyName, out property);
    }

    public static bool HasProperty<TProperty>(
        this Type that,
        string propertyName,
        out PropertyInfo property)
    {
        property =
            that.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .SingleOrDefault(
                    pi => pi.Name == propertyName &&
                          typeof(TProperty).IsAssignableFrom(pi.PropertyType));
        return property != null;
    }

    public static bool HasAttribute<TAttribute>(this Type that)
        where TAttribute : Attribute
    {
        return that.IsDefined(typeof(TAttribute));
    }

    #endregion
}