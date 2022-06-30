/*****************************************************************************/
/* Build  : 27-Jun-2022                                                      */
/* Author : www.kn.team                                                      */
/*****************************************************************************/
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Akashic.Utilities.Extensions
{
    public static class FieldInfoExtensions
    {
        public static List<FieldInfo> GetConstants(this Type type)
        {
            FieldInfo[] fieldInfos = type.GetFields(
                BindingFlags.Public |
                BindingFlags.Static | 
                BindingFlags.FlattenHierarchy);

            var result = fieldInfos.Where(fi => fi.IsLiteral && !fi.IsInitOnly).ToList();
            return result;
        }

        public static string GetFieldInfoName(this FieldInfo info)
        {
            var result = info?.GetValue(null)?.ToString() ?? string.Empty;
            return result;
        }

        public static string GetFieldInfoDisplayAttribute(this FieldInfo info)
        {
            var result = string.Empty;
            var atts = info.GetCustomAttributes(typeof(DisplayAttribute), true);

            if (atts.Length == 0)
                return result;

            result = (atts[0] as DisplayAttribute)?.Name ?? result;
            return result;
        }
    }
}
