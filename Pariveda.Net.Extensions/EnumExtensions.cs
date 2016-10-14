using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Pariveda.Net.Extensions
{
    public static class EnumExtensions
    {
        public static string GetFriendlyName(this Enum e)
        {
            DescriptionAttribute attribute = GetAttributeOrDefault<DescriptionAttribute>(e);

            string name = Enum.GetName(e.GetType(), e);
            if (name != null)
            {
                if (attribute != null)
                {
                    return attribute.Description;
                }
                return Enum.GetName(e.GetType(), e);
            }

            return string.Empty;
        }

        public static Expected GetAttributeValueOrDefault<T, Expected>(this Enum e, Func<T, Expected> expression)
        where T : System.Attribute
        {
            T attribute = GetAttributeOrDefault<T>(e);

            if (attribute == null)
                return default(Expected);

            return expression(attribute);
        }

        public static T GetAttributeOrDefault<T>(this Enum e) where T : System.Attribute
        {
            var firstOrDefault = e
                .GetType()
                .GetMember(e.ToString())
                .FirstOrDefault(member => member.MemberType == MemberTypes.Field);
            if (firstOrDefault != null)
            {
                T attribute = firstOrDefault
                    .GetCustomAttributes(typeof(T), false)
                    .Cast<T>()
                    .SingleOrDefault();

                return attribute;
            }

            return null;
        }
    }
}
