using FWCCLISongReporting.ChristianHymns;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ChristianHymnsCCLISongNumber.Comparison;

namespace ChristianHymnsCCLISongNumber
{
    class ComparableSongContributors
    {
        private readonly IList<SongContributor> Authors;

        public ComparableSongContributors(IList<SongContributor> authors)
        {
            this.Authors = authors;
        }

        /// <summary>
        /// Compared the input with the current SongContributor
        /// The implementation currently accepts fuzzy matches
        /// </summary>
        /// <param name="authorsList">a string list of authors</param>
        /// <param name="acceptFuzzy">true if a partial match is acceptable</param>
        /// <returns>author lists are similar/same depending on acceptFuzzy value</returns>
        public bool Compare(Name remoteName, bool acceptFuzzy = false)
        {
            var localName = new Name(this.NormalisedStringWithoutMiddleNames());
            throw new NotImplementedException();
            /*
            if (localName.ToLowerAndRemoveSymbolsAndTrim().ToString()
                == remoteName.ToLowerAndRemoveSymbolsAndTrim().ToString())
            {
                return true;
            }

            if (localName.WithoutSingleLetterMiddleNames().ToLowerAndRemoveSymbolsAndTrim().ToString()
                == remoteName.WithoutSingleLetterMiddleNames().ToLowerAndRemoveSymbolsAndTrim().ToString())
            {
                return true;
            }
            */
            return false;
        }



        public string NormalisedStringWithMiddleNames()
        {
            return String.Join(" ", this.NamesToList(true).ToArray());
        }

        public string NormalisedStringWithoutMiddleNames()
        {
            return String.Join(" ", this.NamesToList(false).ToArray());
        }

        private IList<string> NamesToList(bool includeMiddleName)
        {
            var authors = new List<string>();
            foreach (var item in this.Authors)
            {
                if ("" != item.FirstName())
                {
                    authors.Add(item.FirstName());
                }
                if (includeMiddleName && "" != item.MiddleNamesOrParts())
                {
                    authors.AddRange(item.MiddleNamesOrParts().Split(' '));
                }
                if ("" != item.LastName())
                {
                    authors.Add(item.LastName());
                }
            }
            authors.Sort();
            return authors;
        }

        private bool FuzzyMiddleNameMatch(Name remoteAuthorsAsStringWithSpaces, Name authorsWithMiddleNamesAsStringWithSpaces)
        {
            var remote = remoteAuthorsAsStringWithSpaces.WithoutSingleLetterMiddleNames().ToString().Split(' ');
            var local = authorsWithMiddleNamesAsStringWithSpaces.WithoutSingleLetterMiddleNames().ToString().Split(' ');

            if (remote.Length - 1 == local.Length || local.Length - 1 == remote.Length)
            {
                var newList = remote.ToList();
                newList.AddRange(local);
                var distinctValues = newList.Distinct().ToList();
                var resultA = distinctValues.Where(a => local.Any(b => a.ToLower() == b.ToLower()));
                var resultB = distinctValues.Where(a => remote.Any(b => a.ToLower() == b.ToLower()));

                return Math.Abs(resultA.Count() - resultB.Count()) == 1;
            }
            return false;
            
        }

        /// <summary>
        /// Compares 2 strings, by calling trim(), ToLower() and removing any non- A-Za-z characters
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private bool CompareStringsIsEqual(string a, string b)
        {
            throw new NotImplementedException();
            /*
            string normalisedA = new Name(a).ToLowerAndRemoveSymbolsAndTrim().WithoutSpaces();
            string normalisedB = new Name(b).ToLowerAndRemoveSymbolsAndTrim().WithoutSpaces();
            return normalisedA == normalisedB;
            */
        }

    }
}
