using System;
using System.Reflection;

namespace Domain.Common.Extensions
{
    public static class EntityExtensions
    {
        public static string GetEntityName(this Object obj)
        {
            var objType = obj.GetType();

            return GetEntityName(objType);
        }

        public static string GetEntityName(this Type type)
        {
            return (string)type
                .GetField("ENTITY_NAME", BindingFlags.Static | BindingFlags.Public)?
                .GetValue(null)
                    ?? type.Name;
        }
    }
}