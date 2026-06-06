using System.Reflection;
using Valsy.Domain.Common.Enums;

namespace Valsy.Domain.Common.Extensions
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    sealed class DisplayNameAttribute : Attribute
    {
        public string DisplayName { get; }

        public DisplayNameAttribute(string displayName) => DisplayName = displayName;
    }

    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            var type = enumValue.GetType();
            var memberInfo = type.GetMember(enumValue.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                var attrs = memberInfo[0].GetCustomAttributes(typeof(DisplayNameAttribute), false);
                if (attrs != null && attrs.Length > 0)
                {
                    return ((DisplayNameAttribute)attrs[0]).DisplayName;
                }
            }
            return enumValue.ToString();
        }
        public static string GetEnumDisplayName<T>(this T status, Language language) where T : Enum
        {
            FieldInfo fieldInfo = status.GetType().GetField(status.ToString());

            DisplayNameAttribute[] attributes = (DisplayNameAttribute[])fieldInfo.GetCustomAttributes(typeof(DisplayNameAttribute), false);

            return attributes.Length > 0 && language == Language.Ar ? attributes[0].DisplayName : status.ToString();
        }
    }
}
