using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FWCCLISongReporting.ChristianHymns
{
    /**
     * Parses a copyright line, exposing details about the copyright
     * 
     */
    public class SongCopyright
    {
        private string copyrightLine;

        private readonly List<SongContributor> contributors = new List<SongContributor>();

        public SongCopyright(string rawCopyrightLine)
        {
            //todo: How to represent "and others" in the SongContributor list
            this.copyrightLine = rawCopyrightLine.Replace(", and others", "")
                .Replace("from ", "");
            

            string[] parts = null;
            if (this.copyrightLine.Contains(";"))
            {
                parts = this.copyrightLine.Split(';');
            } else if (this.copyrightLine.Contains("&"))
            {
                parts = this.copyrightLine.Split(';');
            }
            else
            {
                parts = this.copyrightLine.Replace(" and ", "|").Split('|');
            }

            foreach (var part in parts)
            {
                contributors.Add(new SongContributor(part));
            }

        }

        private Match Match()
        {
            return new Regex(@"^([A-Za-z'*\- ]+),?( [bdc.]+\.)?( [0-9]{0,4})?(-([0-9]{0,4}))?,?(, vv?. [0-9]-?[0-9]?)?( © [\w\d&©!,.\- /]+)?[\,*]?$")
                .Match(copyrightLine);
        }

        public IList<SongContributor> SongContributors()
        {
            return this.contributors;
        }

        public String Copyright()
        {
            return this.Match().Groups[7].Value.Trim();
        }

        public bool isPublicDomain()
        {
            var isPublicDomain = true;
            foreach (var item in this.SongContributors())
            {
                if ("" == item.Death())
                {
                    if (item.ContributionType() == ContributionType.Book)
                    {
                        var matchedGroups = new Regex(@"([0-9]{4})")
                            .Match(item.ToString())
                            .Groups;
                        if (matchedGroups.Count > 0)
                        {
                            if (Int32.TryParse(matchedGroups[0].Value, out int result))
                            {
                                if (result <= DateTime.Now.Year - 70)
                                {
                                    return true;
                                }
                            }                           
                        }                            
                    }
                    else
                    {
                        if (Int32.TryParse(item.Born(), out int result))
                        {
                            // If they were born 150 years ago, and there is no
                            //    death date, it's safe to assume it's PD!
                            if (result < DateTime.Now.Year - 150)
                            {
                                return true;
                            }
                        }
                    }
                    return false;
                } else
                {
                    if (item.Name() == "Anonymous")
                    {
                        return true;
                    }
                }
                
                int yearOfDeath = Int32.Parse(item.Death());
                if (yearOfDeath > DateTime.Now.Year - 70)
                {
                    return false;
                }
            }
            return isPublicDomain;
        }

        public override string ToString()
        {
            return this.copyrightLine;
        }
    }
}
