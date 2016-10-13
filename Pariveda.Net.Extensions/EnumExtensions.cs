using System;
using System.ComponentModel;
using System.Linq;

namespace Pariveda.Net.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum enumValue)
        {
            // Get the field within the enum
            var memberInfo = enumValue.GetType().GetMember(enumValue.ToString());

            // If any members were found
            if (memberInfo != null && memberInfo.Any())
            {
                // Get any description attributes on member
                var descriptionAttributes =
                    memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

                if (descriptionAttributes != null && descriptionAttributes.Any())
                {
                    return descriptionAttributes.First().Description;
                }
            }

            // If no member found or member does not have description attribute, return the default to string value
            return enumValue.ToString();
        }
    }
}
