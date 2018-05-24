using System.Linq;
using System.Text;

namespace GoogleSpeechApi.Extensions
{
    public static class StringExtensions
    {
        public static string PopWord(this string input, out string remainingString)
        {
            var words = input.Split(' ').ToList();
            remainingString = words.Skip(1).JoinToString(" ");
            return words.FirstOrDefault();
        }

        public static string PeekWord(this string input)
        {
            return input.Split(' ').FirstOrDefault();
        }

        public static string RemoveChars(this string input, params string[] chars )
        {
            string res = input;
            chars.ForEach(c => res = res.Replace(c, ""));
            return res;
        }

        public static string UpperNormalize(this string s)
        {
            var sb = new StringBuilder();
            foreach (char c in s)
            {
                if (char.IsLetterOrDigit(c))
                    sb.Append(c);
            }

            return sb.ToString().ToUpper();
        }
    }
}