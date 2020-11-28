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

        private static Dictionary<int, CCLIReporting.Song> magicList;

        public static Dictionary<int, CCLIReporting.Song> load()
        {
            if (magicList != null)
            {
                return magicList;
            }
            magicList = new Dictionary<int, CCLIReporting.Song>
            {
                // Extracted title from CH is wrong so a valid match is never possible
                { 1, new CCLIReporting.Song("1054810", true) },
                // CCLI contains music author with words (Old Hundredth)
                { 2, new CCLIReporting.Song("3483774", true) },
                // Chandler is credited as translator, Latin 6th Cent original
                { 4, new CCLIReporting.Song("2780447", true) },
                { 7, new CCLIReporting.Song("2623968", false) },
                // Correct value too low down list
                { 16, new CCLIReporting.Song("2682930", true) },
                // No author in CCLI Song listing
                { 27, new CCLIReporting.Song("4212889", true) },
                // "and others" so Author match fail
                { 28, new CCLIReporting.Song("2648022", true) },
                // Many authors
                { 38, new CCLIReporting.Song("744183", true) },
                // Different title
                { 56, new CCLIReporting.Song("3254271", true) },
                // Translator
                { 70, new CCLIReporting.Song("5119747", false) },
                // No author listed in CH
                { 82, new CCLIReporting.Song("27903", true) },
                // Complex Author parsing
                { 184, new CCLIReporting.Song("916557", false) },
                // Now in the public domain
                { 235, new CCLIReporting.Song("507045", false) },

                // Copyright control
                { 238, new CCLIReporting.Song("4580195", true) },
                // Different middlename + low search list
                { 245, new CCLIReporting.Song("3700749", true) },
                // Spelling error in name
                { 248, new CCLIReporting.Song("1589969", true) },
                // CCLI Song contains middle name
                { 249, new CCLIReporting.Song("4044349", true) },
                // Far down list
                { 318, new CCLIReporting.Song("7080134", true) },
            };

            return magicList;
        }
    }
}
