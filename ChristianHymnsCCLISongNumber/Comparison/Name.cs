using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChristianHymnsCCLISongNumber.Comparison
{
    /// <summary>
    /// Provides key processing on a name.
    /// 
    /// A name could be 
    /// Aaron A. Aaronson
    /// 
    /// but it may also be
    /// 
    /// Aaron A. Aaronson Betty B. Bettyson
    /// 
    /// without any delimiter between names. 
    /// </summary>
    class Name
    {
        private string name;

        public Name(string name) : this(name.Split(' ').ToList())
        { }

        public Name(List<string> names)
        {
            names.Sort();
            this.name = String.Join(" ", names);
        }

        /// <summary>
        /// Removes middle initals like A. or Z. from
        /// Aaron A. Aaronson
        /// Betty B. Batterson
        /// 
        /// The method is case insensitive. A. or B.
        /// </summary>
        /// <param name="line">A line of names in a list</param>
        /// <returns></returns>
        public Name WithoutSingleLetterMiddleNames()
        {
            Regex removeMiddleNames = new Regex(@"([A-Z]{1,1}\.)");
            return new Name(removeMiddleNames.Replace(this.name, ""));
        }

        public override string ToString()
        {
            return this.name;
        }
    }
}
