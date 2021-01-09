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
            var words = text.SplitStringIntoSeparateWords();
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



        private static IEnumerable<string> SplitStringIntoSeparateWords(this string selectedText)
        {
            int wordFirstIndex = 0;
            for (int i = 0; i < selectedText.Length; ++i)
            {
                if (!char.IsLetterOrDigit(selectedText[i]))
                {
                    int wordLength = i - wordFirstIndex;
                    if (wordLength > 0)
                    {
                        yield return selectedText.Substring(wordFirstIndex, wordLength);
                    }
                    wordFirstIndex = i + 1;
                }
                if (char.IsUpper(selectedText[i]))
                {
                    int wordLength = i - wordFirstIndex;
                    if (wordLength > 0)
                    {
                        yield return selectedText.Substring(wordFirstIndex, wordLength);
                    }
                    wordFirstIndex = i;
                }
            }
            int remainderLength = selectedText.Length - wordFirstIndex;
            if (remainderLength > 0)
            {
                yield return selectedText.Substring(wordFirstIndex, remainderLength);
            }
        }
    }
}