using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristianHymnsCCLISongNumber
{
    /// <summary>
    /// This static class contains a Dictionay of SongIDs and
    /// CCLI Song numbers where it's not worth trying to 
    /// create a magic regex for.
    /// 
    /// E.G. 
    /// 1. The title is the title of the section, not the song
    /// 2. CCLI lists the Song with the tune
    /// 3. Author is credited as translator
    /// 27. No author in CCLI Song listing
    /// In all cases, there is not sensible way of identifying the
    /// correct version via code (or it's not reasonable too as a
    /// one-off task!)
    /// </summary>
    class HymnMatches
    {

        private static Dictionary<int, string> magicList;

        public static Dictionary<int, string> load()
        {
            if (magicList != null)
            {
                return magicList;
            }
            magicList = new Dictionary<int, string>
            {
                // Extracted title from CH is wrong so a valid match is never possible
                { 1, "1054810" },
                // CCLI contains music author with words (Old Hundredth)
                { 2, "746215" },
                // Chandler is credited as translator, Latin 6th Cent original
                { 4, "2780447" },
                { 7, "2623968" },
                // No author in CCLI Song listing
                { 27, "4212889" },
                // "and others" so Author match fail
                { 28, "2648022" },
                // Many authors
                { 38, "744183" },
                // Different title
                { 56, "3254271" },
                // Translator
                { 70, "5119747" },
                // 82 is in the public domain but is currently misparsed
                // so Public Domain flag is not correctly set
                //{ 82, "27903" }
                // Complex Author parsing
                { 184, "916557" },
                // Now in the public domain
                { 235, "507045" },
                // Copyright control
                { 238, "4580195" },
                // Different middlename
                { 245, "25297" },
                { 248, "1589969" },
                { 249, "4044349" },
                { 318, "7004458" },
            };

            return magicList;
        }
    }
}
