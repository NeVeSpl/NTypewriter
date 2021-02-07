using System.Collections.Generic;
using System.Linq;

namespace NTypewriter.CodeModel.Functions
{
    /// <summary>
    /// String functions
    /// </summary>
    public static class StringFunctions
    {
        /// <summary>
        /// Converts text case to CamelCase
        /// </summary>
        public static string ToCamelCase(this string text)
        {
            var words = text.SplitIntoSeparateWords();
            return string.Join("", words.Select(x => x.ToLower().ToUpperFirst())).ToLowerFirst();
        }


        /// <summary>
        /// Converts first letter of the given string to upper case
        /// </summary>
        public static string ToUpperFirst(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            char[] a = text.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }


        /// <summary>
        /// Converts first letter of the given string to lower case
        /// </summary>
        public static string ToLowerFirst(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            char[] a = text.ToCharArray();
            a[0] = char.ToLower(a[0]);
            return new string(a);
        }



        public static IEnumerable<string> SplitIntoSeparateWords(this string text)
        {
            int wordFirstIndex = 0;
            for (int i = 0; i < text.Length; ++i)
            {
                if (!char.IsLetterOrDigit(text[i]))
                {
                    int wordLength = i - wordFirstIndex;
                    if (wordLength > 0)
                    {
                        yield return text.Substring(wordFirstIndex, wordLength);
                    }
                    wordFirstIndex = i + 1;
                }
                if (char.IsUpper(text[i]))
                {
                    int wordLength = i - wordFirstIndex;
                    if (wordLength > 0)
                    {
                        yield return text.Substring(wordFirstIndex, wordLength);
                    }
                    wordFirstIndex = i;
                }
            }
            int remainderLength = text.Length - wordFirstIndex;
            if (remainderLength > 0)
            {
                yield return text.Substring(wordFirstIndex, remainderLength);
            }
        }
    }
}