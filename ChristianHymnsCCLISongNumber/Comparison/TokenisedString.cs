using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChristianHymnsCCLISongNumber.Comparison
{
    class TokenisedString
    {
        private readonly string tokens;

        /// <summary>
        /// Encapsulates a string for comparison. Each token is delimited by ' '
        /// </summary>
        /// <param name="tokens">A string for comparison</param>
        public TokenisedString(string tokens) : this(tokens.Split(' ').ToList())
        { }

        /// <summary>
        /// Encapsulates a string for comparison. A custom set of delimieters (passed to string.Split())
        /// </summary>
        /// <param name="tokens"></param>
        /// <param name="separator"></param>
        public TokenisedString(string tokens, params char[] separator) : this(tokens.Split(separator).ToList())
        { }

        public TokenisedString(List<string> tokens)
        {
            tokens.Sort();
            this.tokens = String.Join(" ", tokens);
        }

        public TokenisedString ToLowerAndRemoveSymbolsAndTrim()
        {            
            return new TokenisedString(
                this.tokens.Replace(this.tokens.Trim().ToLower(), "")
                )
                .WithoutRegex("[^a-zA-Z ]");
        }

        public TokenisedString RemoveSpaces()
        {
            return new TokenisedString(this.tokens.Replace(" ", String.Empty));
        }

        public TokenisedString WithoutString(String term)
        {
            return new TokenisedString(this.tokens.Replace(term, string.Empty));
        }

        public TokenisedString WithoutRegex(string term)
        {
            Regex rgx = new Regex(term);
            return new TokenisedString(rgx.Replace(this.tokens, ""));
        }

    }
}
