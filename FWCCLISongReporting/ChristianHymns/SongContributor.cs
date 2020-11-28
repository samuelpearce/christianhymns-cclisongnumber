using System.Linq;
using System;
using System.Text.RegularExpressions;

namespace FWCCLISongReporting.ChristianHymns
{
    public class SongContributor
    {
        private string modifiedCopyright;
        private string originalCopyright;

        private ContributionType type;

        /// <summary>
        /// Creates a new SongContributor based on an input string
        /// </summary>
        /// <param name="modifiedCopyright">A copyright string containing 1 author</param>
        public SongContributor(string modifiedCopyright) : this(modifiedCopyright, modifiedCopyright)
        {           
        }

        /// <summary>
        /// Creates a new SongContributor based on an input string
        /// </summary>
        /// <param name="modifiedCopyright">A copyright string containing 1 author</param>
        /// <param name="originalCopyright">The original copyright string where an explicit partial removal is required</param>
        public SongContributor(string modifiedCopyright, string originalCopyright)
        {
            modifiedCopyright = modifiedCopyright.Trim()
                // input has lowercase L instead of 1 so regex breaks
                .Replace("Nicolaus Ludwig von Zinzendorf, 1700-60, v. l",
                    "Nicolaus Ludwig von Zinzendorf, 1700-60, v. 1")
                .Replace("Graham D S Deans", "Graham DS Deans");

            // " Text (mod.)" - remove and manually handle
            // "as in Scottish Paraphrases" is appendix data
            // "From [A-Z ](, [0-9]{4})? is usually from a book
            // "By permission pf" followed by a copyright text
            // Copyright Control (no (c) so difficult to match
            // [Cc]ent - e.g. Latin 12th Cent.

            // Once removed, parse text like usual
            // starts with "tr. by" is a translation
            // "v. [0-9]?[0-9]{0,9}  by" is a specific author for a verse

            modifiedCopyright = this.SetContributionTypeAndRemoveString(
                modifiedCopyright, "tr. by ", "", ChristianHymns.ContributionType.Translator);
            modifiedCopyright = this.SetContributionTypeAndRemoveString(
                modifiedCopyright, "alt. by", "", ChristianHymns.ContributionType.Alterer);
            modifiedCopyright = this.SetContributionTypeAndRemoveString(
                modifiedCopyright, "altd. by", "", ChristianHymns.ContributionType.Alterer);            
            modifiedCopyright = this.SetContributionTypeAndRemoveString(
                modifiedCopyright, "translated and adapted by ", "", ChristianHymns.ContributionType.Alterer);
            modifiedCopyright = this.SetContributionTypeAndRemoveString(
                modifiedCopyright, "translated and adapted by", "", ChristianHymns.ContributionType.Alterer);

            modifiedCopyright = modifiedCopyright.Replace(" and ", "");
          
            this.modifiedCopyright = modifiedCopyright;
            this.originalCopyright = originalCopyright;

            var nameOrig = this.Name();
            var upper = this.Name().ToUpper();

            if (upper != string.Empty && upper == nameOrig)
            {
                type = ChristianHymns.ContributionType.Book;
            }

            if (type == null)
            {
                this.type = ChristianHymns.ContributionType.Author;
            }

        }

        private string SetContributionTypeAndRemoveString(string copyrightString, string strToMatch, string replace, ContributionType type)
        {
            var modified = copyrightString.Replace(strToMatch, replace);
            if (modified != copyrightString)
            {
                this.type = type;
            }
            return modified;
        }

        private Match Match()
        {           
            return new Regex(@"^([A-Za-z*.\- ]+),?( [bdc.]+\.)?( [0-9]{0,4})?(-([0-9]{0,4}))?(, vv?. [0-9]-?[0-9]?)?( © [\w\d&©,. /]+)?$")
                .Match(modifiedCopyright);
        }

        public String FirstName()
        {
            var parts = this.Name().ToString().Split(' ');
            if (parts.Length > 1)
            {
                return parts.First();
            }
            else
            {
                return string.Empty;
            }            
        }

        /**
         * Returns the middle name, initals or an empty string if none given
         */
        public String MiddleNamesOrParts()
        {
            var parts = this.Name().Split(' ').ToList();
            if (parts.Count() > 0)
            {
                parts.RemoveAt(0);
            }
            if (parts.Count() > 0)
            {
                parts.RemoveAt(parts.Count() - 1);
            }
            return String.Join(" ", parts);
        }

        public String LastName()
        {
            return this.Name().Split(' ').Last();
        }

        public String Name()
        {
            return this.Match().Groups[1].Value.ToString().Trim();
        }

        public String Born()
        {
            return this.Match().Groups[3].Value.ToString().Trim();
        }

        public String Death()
        {
            var date = this.Match().Groups[5].Value.ToString().Trim();
            if (date.Length == 2)
            {
                var born = this.Born();
                var prefix = "";
                if (born.Length == 3)
                {
                    prefix = born.Substring(0, 1);
                }
                else if (born.Length == 4)
                {
                    prefix = born.Substring(0, 2);
                }
                date = prefix + date;
            }
            return date;
        }

        public bool IsAlive()
        {
            return null == this.Death() || string.Empty == this.Death();
        }

        public String Copyright()
        {
            return this.Match().Groups[7].Value.Trim();            
        }

        public ContributionType ContributionType()
        {
            return this.type;
        }

        public string FirstLastName()
        {
            return this.FirstName() + " " + this.LastName();
        }
        
        public string ToString()
        {
            return this.originalCopyright;
        }
    }
}