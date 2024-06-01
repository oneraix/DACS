using System.ComponentModel.DataAnnotations;
using System;
using System.Linq;
using System.Reflection;

namespace DACS.Models
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetField(enumValue.ToString())
                            ?.GetCustomAttributes(typeof(DisplayAttribute), false)
                            .Cast<DisplayAttribute>()
                            .FirstOrDefault()?.Name ?? enumValue.ToString();
        }
    }
}
