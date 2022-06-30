/*****************************************************************************/
/* Build  : 15-01-2013                                                       */
/* Update : 24-06-2022                                                       */
/* Author : www.kn.team                                                      */
/*****************************************************************************/
using System.Globalization;
using System.Text.RegularExpressions;

namespace Akashic.Utilities.Extensions
{
    public static class StringExtensions
    {
        public static string Right(this string text, int length)
        {
            var result = text[^length..];
            return result;
        }

        public static string Left(this string text, int length)
        {
            var result = text[..length];
            return result;
        }

        public static string Reverse(this string text)
        {
            var arr = text.ToCharArray();
            Array.Reverse(arr);
            var result = new string(arr);

            return result;
        }

        public static string TitleCase(this string text)
        {
            var culture = CultureInfo.CurrentCulture.TextInfo;
            var result = culture.ToTitleCase(text);
            return result;
        }

        public static string ForceTitleCase(this string text)
        {
            var result = text;

            if (!string.IsNullOrEmpty(result))
            {
                var words = text.Split(' ');
                for (int i = 0; i < words.Length; i++)
                {
                    if (words[i].Length == 0)
                        continue;

                    var firstChar = char.ToUpper(words[i][0]);
                    var rest = "";

                    if (words[i].Length > 1)
                    {
                        rest = words[i].Substring(1).ToLower();
                    }
                    words[i] = firstChar + rest;
                }
                result = string.Join(" ", words);
            }

            return result;
        }

        //

        public static string[] SplitWords(this string text)
        {
            var result = Regex.Split(text.Trim(), @"\W+");
            return result;
        }

        //

        public static string RemoveInternalSpaces(this string text, char[]? chars = null)
        {
            chars = (chars == null || chars.Length < 1) ? new char[] { ' ', '\n', '\t', '\r', '\f', '\v', '-', '_' } : chars;
            string? result = string.Join(" ", text.Split(chars, StringSplitOptions.RemoveEmptyEntries));
            return result;
        }

        public static string RemoveSpecialChars(this string text, string[]? chars = null)
        {
            chars = (chars == null || chars.Length < 1) ?
                    new string[] { "!", "@", "#", "$", "%", "^", "&", "*", "'", "/", @"\", ";", "(", ")", ":", "|", "[", "]", "=", "~", "{", "}", "<", ">", "?", "+", ",", ".", "-", "_" } : chars;

            for (int i = 0; i < chars.Length; i++)
            {
                if (text.Contains(chars[i]))
                {
                    text = text.Replace(chars[i], "");
                }
            }
            return text;
        }

        //
        
        /// <summary>
        /// Convert with format = "dd/MM/yyyy" or format = "dd/MM/yy"
        /// </summary>
        public static DateTime ToDateTime(this string text, string format)
        {
            var result = DateTime.ParseExact(text, format, CultureInfo.InvariantCulture);
            return result;
        }

        /// <summary>
        /// Usage : Color colorEnum = "Red".ToEnum<Color>();
        /// </summary>
        public static T ToEnum<T>(this string text)
        {
            var result = (T)Enum.Parse(typeof(T), text);
            return result;
        }

        /// <summary>
        /// Usage :  "1".ToType<int>()
        /// </summary>
        public static T ToType<T>(this string text)
        {
            var result = (T)Convert.ChangeType(text, typeof(T));
            return result;
        }

        /// <summary>
        /// <para>More convenient than using T.TryParse(string, out T). 
        /// Works with primitive types, structs, and enums.
        /// Tries to parse the string to an instance of the type specified.
        /// If the input cannot be parsed, null will be returned.
        /// </para>
        /// <para>
        /// If the value of the caller is null, null will be returned.
        /// So if you have "string s = null;" and then you try "s.ToNullable...",
        /// null will be returned. No null exception will be thrown. 
        /// </para>
        /// <author>Contributed by Taylor Love (Pangamma)</author>
        /// </summary>
        public static T? ToNullable<T>(this string text) where T : struct
        {
            if (!string.IsNullOrEmpty(text))
            {
                var converter = System.ComponentModel.TypeDescriptor.GetConverter(typeof(T));

                if (converter.IsValid(text))
#pragma warning disable CS8605 // Unboxing a possibly null value.
                    return (T)converter.ConvertFromString(text);
#pragma warning restore CS8605 // Unboxing a possibly null value.

                if (typeof(T).IsEnum)
                {
                    if (Enum.TryParse(text, out T t))
                        return t;
                }
            }

            return null;
        }

        /// <summary>
        /// Decode &lt;b&gt;Hello &#39;friend&#39;&lt;/b&gt; to <b>Hello 'friend'</b>
        /// </summary>
        public static string DecodeHtml(this string text)
        {
            var result = System.Net.WebUtility.HtmlDecode(text);
            return result;
        }

        //

        public static List<T> SplitToList<T>(this string text, char[] delimiters, bool isOnly = false)
        {
            var result = new List<T>();

            if (!string.IsNullOrWhiteSpace(text))
            {
                var parts = text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                foreach (var item in from string part in parts
                                     let item = ToType<T>(part)
                                     select item)
                {
                    if (isOnly)
                    {
                        if (!result.Contains(item))
                        {
                            result.Add(item);
                        }
                    }
                    else
                    {
                        result.Add(item);
                    }
                }
            }

            return result;
        }

        //

        public static int CountStringOccurrences(this string text, string pattern)
        {
            var result = 0;
            var i = 0;
            while ((i = text.IndexOf(pattern, i)) != -1)
            {
                i += pattern.Length;
                result++;
            }
            return result;
        }

        public static bool IsTandemRepeat(this string text)
        {
            var result = false;

            if(string.IsNullOrEmpty(text))
                return result;

            var FindDup = new Regex(@"(.+)\1", RegexOptions.IgnoreCase);
            var allMatches = FindDup.Matches(text);

            result = (allMatches.Count > 0) && (text.Length % (allMatches.Count + 1) == 0);
            return result;
        }

        /// <summary>
        /// Usage : if (text.StartsWithAny(new List<string>() { "A", "B", "C" })) do something
        /// </summary>
        public static bool StartsWithAny(this string text, IEnumerable<string> strings)
        {
            foreach (var valueToCheck in strings)
            {
                if (text.StartsWith(valueToCheck))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Usage : 
        /// string word = "AWord";
        /// string startsWithValue;
        /// if (word.StartsWithAny(new List<string>() { "a", "b", "c" }, out startsWithValue))
        /// {
        ///    switch (startsWithValue)
        ///    {
        ///        case "A": Do Something
        ///    }
        /// }
        /// </summary>
        public static bool StartsWithAny(this string text, IEnumerable<string> strings, out string startsWithValue)
        {
            startsWithValue = string.Empty;

            foreach (var valueToCheck in strings)
            {
                if (text.StartsWith(valueToCheck))
                {
                    startsWithValue = valueToCheck;
                    return true;
                }
            }

            return false;
        }
    }
}
