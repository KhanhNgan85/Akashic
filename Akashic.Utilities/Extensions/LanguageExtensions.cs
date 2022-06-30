/*****************************************************************************/
/* Build  : 26-Jun-2022                                                       */
/* Author : www.kn.team                                                      */
/*****************************************************************************/
using System.Globalization;
using System.Text;

namespace Akashic.Utilities.Extensions
{
    public static class LanguageExtensions
    {
        public static string Standarize(this string text)
        {
            bool[] _lookup = new bool[65535];
            for (char c = '0'; c <= '9'; c++) _lookup[c] = true;
            for (char c = 'A'; c <= 'Z'; c++) _lookup[c] = true;
            for (char c = 'a'; c <= 'z'; c++) _lookup[c] = true;
            _lookup['.'] = true;
            _lookup['_'] = true;
            _lookup[' '] = true;

            char[] _vietnam = new char[] {
                'á', 'à', 'ả', 'ã', 'ạ',
                'ấ', 'ầ', 'ẩ', 'ẫ', 'ậ', 'â',
                'ắ', 'ằ', 'ẳ', 'ẵ', 'ặ', 'ă',
                'ó', 'ò', 'ỏ', 'õ', 'ọ',
                'ố', 'ồ', 'ổ', 'ỗ', 'ộ', 'ô',
                'ớ', 'ờ', 'ở', 'ỡ', 'ợ', 'ợ',
                'é', 'è', 'ẻ', 'ẽ', 'ẹ',
                'ế', 'ề', 'ể', 'ễ', 'ệ', 'ê',
                'í', 'ì', 'ỉ', 'ĩ', 'ị',
                'ý', 'ỳ', 'ỷ', 'ỹ', 'ỵ',
                'ú', 'ù', 'ủ', 'ũ', 'ụ',
                'ứ', 'ừ', 'ử', 'ữ', 'ự',
                'đ'
            };

            foreach (char c in _vietnam)
            {
                _lookup[c] = true;
                _lookup[char.ToUpper(c)] = true;
            }

            char[] buffer = new char[text.Length];
            int index = 0;
            foreach (char c in text)
            {
                if (_lookup[c])
                {
                    buffer[index] = c;
                    index++;
                }
            }
            var result = new string(buffer, 0, index);

            return result;
        }

        public static string RemoveVietnameseString(this string text)
        {
            text = text.Replace('đ', 'd');
            text = text.Replace('Đ', 'D');
            string stFormD = text.Normalize(NormalizationForm.FormD);
            int len = stFormD.Length;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < len; i++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[i]);
                if (uc != UnicodeCategory.NonSpacingMark) { sb.Append(stFormD[i]); }
            }
            return (sb.ToString().Normalize(NormalizationForm.FormC));
        }
    }
}
