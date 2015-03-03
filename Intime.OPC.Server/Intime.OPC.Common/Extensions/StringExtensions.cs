
namespace System
{
    using Collections.Generic;

    using Globalization;

    using Linq;

    using Security.Cryptography;

    using Text;
    using Text.RegularExpressions;

    public static class HashHelper
    {
        #region Methods

        public static string CalculateMD5Hash(this string input)
        {
            return CalculateHash(input, MD5.Create());
        }

        public static string CalculateSHA1Hash(this string input)
        {
            return CalculateHash(input, SHA1.Create());
        }

        private static string CalculateHash(string input, HashAlgorithm hashAlgorithm)
        {
            byte[] inputBytes;
            byte[] hash;

            using (var hashProvider = hashAlgorithm)
            {
                inputBytes = Encoding.ASCII.GetBytes(input);
                hash = hashProvider.ComputeHash(inputBytes);
            }

            // step 2, convert byte array to hex string
            var sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2", CultureInfo.InvariantCulture));
            }
            return sb.ToString();
        }

        #endregion Methods
    }

    public static class HtmlHelper
    {
        #region Methods

        public static string StripHtml(this string text)
        {
            Regex reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
            return reg.Replace(text, "");
        }

        #endregion Methods
    }

    public static class StringSlugExtension
    {
        private static readonly Regex guidExpression =
            new Regex(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$");

        #region Fields

        private static readonly Dictionary<string, string> rules1;
        private static readonly Dictionary<string, string> rules2;

        #endregion Fields

        #region Constructors

        static StringSlugExtension()
        {
            List<char> invalidChars = "àáâãäåçèéêëìíîïñòóôõöøùúûüýÿ".ToCharArray().ToList();
            List<char> validChars = "aaaaaaceeeeiiiinoooooouuuuyy".ToCharArray().ToList();
            rules1 = invalidChars.ToDictionary(i => i.ToString(), i => validChars[invalidChars.IndexOf(i)].ToString());

            invalidChars = new[] { 'Þ', 'þ', 'Ð', 'ð', 'ß', 'Œ', 'œ', 'Æ', 'æ', 'µ', '&', '(', ')' }.ToList();
            List<string> validStrings =
                new[] { "TH", "th", "DH", "dh", "ss", "OE", "oe", "AE", "ae", "u", "and", "", "" }.ToList();
            rules2 = invalidChars.ToDictionary(i => i.ToString(), i => validStrings[invalidChars.IndexOf(i)]);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Will transform "some $ugly ###url wit[]h spaces" into "some-ugly-url-with-spaces"
        /// </summary>
        public static string Slugify(this string phrase)
        {
            string str = phrase.ToLower();

            // Transform Invalid Chars.
            str = str._StrTr(rules1);

            // Transform Special Chars.
            str = str._StrTr(rules2);

            // Final clean up.
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");

            // convert multiple spaces/hyphens into one space
            str = Regex.Replace(str, @"[\s-]+", " ").Trim();

            // hyphens
            str = Regex.Replace(str, @"\s", "-");

            return str;
        }

        private static string _StrTr(this string source, Dictionary<string, string> replacements)
        {
            var finds = new string[replacements.Keys.Count];

            replacements.Keys.CopyTo(finds, 0);

            string findpattern = string.Join("|", finds);

            var regex = new Regex(findpattern);

            MatchEvaluator evaluator =
                delegate(Match m)
                {
                    string match = m.Captures[0].Value; // either "hello" or "hi" from the original string

                    if (replacements.ContainsKey(match))
                    {
                        return replacements[match];
                    }
                    else
                    {
                        return match;
                    }
                };

            return regex.Replace(source, evaluator);
        }


        public static bool IsNull(this string s) {
            return string.IsNullOrWhiteSpace(s) || string.IsNullOrEmpty(s);
        }
        #endregion Methods


        public static bool IsGuid(this string s)
        {
            return guidExpression.IsMatch(s);
            
        }

        public static Guid ToGuid(this string s)
        {
            return new Guid(s);
        }

        public static int ToInt(this string s) {
            return s.ToInt(0);
        }

        public static int ToInt(this string s,int dafultValue)
        {
            int d = dafultValue;
            bool bl= int.TryParse(s, out d);
            if (!bl)
            {
                 d = dafultValue;
            }
            return d;
        }

        public static double ToDouble(this string s, double dafultValue=0)
        {
            double d = dafultValue;
            bool bl = double.TryParse(s, out d);
            if (!bl)
            {
                d = dafultValue;
            }
            return d;
        }
        /// <summary>
        /// 中文字截字，不足補字串
        /// </summary>
        /// <param name="org">原始字串</param>
        /// <param name="RL">R(右补齐) L(左补齐)</param>
        /// <param name="sLen">长度</param>
        /// <param name="padStr">替代字元</param>
        /// <returns></returns>
        static string CHT_WordPadLeftRight(string org, string RL, int sLen, char padStr)
        {
            var sResult = "";
            //計算轉換過實際的總長
            int orgLen = 0;
            int tLen = 0;
            for (int i = 0; i < org.Length; i++)
            {
                string s = org.Substring(i, 1);
                int vLen = 0;
                //判斷 asc 表是否介於 0~128
                if (Convert.ToInt32(s[0]) > 128 || Convert.ToInt32(s[0]) < 0)
                {
                    vLen = 2;
                }
                else
                {
                    vLen = 1;
                }
                orgLen += vLen;
                if (orgLen > sLen)
                {
                    orgLen -= vLen;
                    break;
                }
                sResult += s;
            }
            //計算轉換過後，最後實際的長度
            tLen = sLen - orgLen;
            string ss = "".PadLeft(tLen);
            if (RL == "L")
            {
                return ss + org;
            }
            else
            {
                return org + ss;
            }
        }


        /// <summary>
        /// 补齐中文字符串长度
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="length">总长度</param>
        /// <param name="padchar">补齐的字符</param>
        /// <returns>System.String.</returns>
        public static string PadLeftCn(this string s, int length,char padchar=' ') {
            return CHT_WordPadLeftRight(s, "L", length, padchar);
        }


        /// <summary>
        /// Pads the right cn.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="length">总长度</param>
        /// <param name="padchar">补齐的字符</param>
        /// <returns>System.String.</returns>
        public static string PadRightCn(this string s, int length, char padchar = ' ')
        {
            return CHT_WordPadLeftRight(s, "R", length, padchar);
        }

        public static int[] ToInts(this string s, char pchar = '%')
        {
            IList<int> lst=new List<int>();
            var strs = s.Split(pchar);
            strs.ForEach((t) =>
            {
                lst.Add(t.ToInt());
            });
            return lst.ToArray();
        }

    }
}