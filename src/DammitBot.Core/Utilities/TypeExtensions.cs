using System;
using System.Linq;
using System.Reflection;

namespace DammitBot.Utilities;

/// <summary>
/// Extensions to the <see cref="Type"/> class.
/// </summary>
public static class TypeExtensions
{
    #region Exposed Methods

    /// <summary>
    /// Check the <see cref="Type"/> for a property named <paramref name="propertyName"/>.  If found,
    /// <paramref name="property"/> will be set to a <see cref="PropertyInfo"/> representing that
    /// property, and true will be returned.  If not found, false will be returned.
    /// </summary>
    public static bool HasProperty(this Type that, string propertyName, out PropertyInfo? property)
    {
        return that.HasProperty<object>(propertyName, out property);
    }

    /// <summary>
    /// Check the <see cref="Type"/> for a property named <paramref name="propertyName"/> of type
    /// <typeparamref name="TProperty"/>.  If found, <paramref name="property"/> will be set to a
    /// <see cref="PropertyInfo"/> representing that property, and true will be returned.  If not found or
    /// the type does not match, false will be returned.
    /// </summary>
    public static bool HasProperty<TProperty>(
        this Type that,
        string propertyName,
        out PropertyInfo? property)
    {
        property =
            that.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .SingleOrDefault(
                    pi => pi.Name == propertyName &&
                          typeof(TProperty).IsAssignableFrom(pi.PropertyType));
        return property != null;
    }

    /// <summary>
    /// Return a boolean value indicating whether or not the <see cref="Type"/> has an attribute of type
    /// <typeparamref name="TAttribute"/>.
    /// </summary>
    public static bool HasAttribute<TAttribute>(this Type that)
        where TAttribute : Attribute
    {
        return that.IsDefined(typeof(TAttribute));
    }

    #endregion
}