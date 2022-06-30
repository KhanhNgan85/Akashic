/*****************************************************************************/
/* Build  : 26-Jun-2022                                                      */
/* Author : www.kn.team                                                      */
/*****************************************************************************/
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Akashic.Utilities.Extensions
{
    public static class EnumExtensions
    {
        public static int GetValue<T>(this T @enum) where T : struct, Enum
        {
            var result = (int)(object)@enum;
            return result;  
        }

        public static string GetName<T>(this T @enum, string defaultValue = "") where T : struct, Enum
        {
            var result = Enum.GetName(typeof(T), @enum) ?? defaultValue;
            return result;
        }

        public static string GetDisplayName<T>(this T @enum, string defaultValue = "") where T : struct, Enum
        {
            var result = @enum.GetType()
                            ?.GetMember(@enum.ToString())
                            ?.First()
                            ?.GetCustomAttribute<DisplayAttribute>()
                            ?.GetName() ?? defaultValue;

            return result;
        }

         /// <summary>
        /// Usage :  Enum.GetValues().Select(v => v.ToString()).ToList();
        /// </summary>
        public static IEnumerable<T> GetValues<T>() where T : struct, Enum
        {
            var result = Enum.GetValues(typeof(T)).Cast<T>();
            return result;
        }

        public static Dictionary<string, int> ToDictionary<T>() where T : struct, Enum
        {
            var result = Enum.GetValues(typeof(T))
                .Cast<T>()
                .ToDictionary(k => k.ToString(), v => (int)(object)v);
            return result;
        }
    }
}
