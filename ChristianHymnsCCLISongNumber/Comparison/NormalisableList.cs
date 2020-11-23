using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChristianHymnsCCLISongNumber.Comparison
{
    class NormalisableList<IList>
    {
        /// <summary>
        /// Normalisable terms, delimited by a space
        /// </summary>
        private string terms;


        public NormalisableList(string spaceDelimitedTerms) : this(spaceDelimitedTerms.Split(' ').ToList())
        { }

        public NormalisableList(List<string> terms)
        {
            terms.Sort();
            this.terms = String.Join(" ", terms);
        }
        public Name ToLowerAndRemoveSymbolsAndTrim()
        {
            Regex rgx = new Regex("[^a-zA-Z ]");
            return new Name(rgx.Replace(this.terms.Trim().ToLower(), ""));
        }
    }
}
