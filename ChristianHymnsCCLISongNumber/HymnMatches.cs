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
                { 6, new CCLIReporting.Song("2952134", false) },
                { 7, new CCLIReporting.Song("2623968", false) },
                // Correct value too low down list
                { 16, new CCLIReporting.Song("2682930", true) },
                // No author in CCLI Song listing
                { 27, new CCLIReporting.Song("4212889", true) },
                // "and others" so Author match fail
                { 28, new CCLIReporting.Song("2648022", true) },
                // Many authors
                { 38, new CCLIReporting.Song("744183", true) },
                { 45, new CCLIReporting.Song("2756361", true) },
                // Different title
                { 56, new CCLIReporting.Song("3254271", true) },
                // Translator
                { 70, new CCLIReporting.Song("5119747", false) },
                // No author listed in CH
                { 82, new CCLIReporting.Song("27903", true) },
                // Title different (o God vs Our God)
                { 115, new CCLIReporting.Song("2648606", true) },
                
                { 118, new CCLIReporting.Song("2953425", true) },
                { 129, new CCLIReporting.Song("2244220", true) },
                // Song title name different to first line
                { 143, new CCLIReporting.Song("38961", false) },
                // Song title name different to first line
                { 144, new CCLIReporting.Song("2492216", false) },                
                // Slightly different name
                { 159, new CCLIReporting.Song("871991", false) },
                // Complex Author parsing
                { 184, new CCLIReporting.Song("916557", false) },
                // Song title name different to first line
                { 193, new CCLIReporting.Song("3709898", false) },
                { 198, new CCLIReporting.Song("3244126", true) },
                // Now in the public domain
                { 235, new CCLIReporting.Song("507045", false) },
                // Different auther
                { 237, new CCLIReporting.Song("2647669", true) },                
                // Copyright control
                { 238, new CCLIReporting.Song("4580195", true) },
                // Different middlename + low search list
                { 245, new CCLIReporting.Song("3700749", true) },
                // Spelling error in name
                { 248, new CCLIReporting.Song("1589969", true) },
                // CCLI Song contains middle name
                { 249, new CCLIReporting.Song("4044349", true) },
                // diffrent title
                { 265, new CCLIReporting.Song("2779685", true) },
                // CCLI contains middle names
                { 273, new CCLIReporting.Song("2424507", true) },
                // Far down list
                { 318, new CCLIReporting.Song("7080134", true) },
                // Anonymous
                { 322, new CCLIReporting.Song("2743628", true) },
                // Contains book source, but public domain
                { 333, new CCLIReporting.Song("3788792", true) },
                // Strange author formatting
                { 351, new CCLIReporting.Song("3168185", true) },
                // Strange author formatting
                { 378, new CCLIReporting.Song("3162143", true) },
                { 365, new CCLIReporting.Song("3240230", false) },
                { 393, new CCLIReporting.Song("2768713", true) },
                { 402, new CCLIReporting.Song("40571", false) },
                { 483, new CCLIReporting.Song("2649935", false) },
                { 491, new CCLIReporting.Song("2993241", true) },
                { 494, new CCLIReporting.Song("3213164", true) },
                { 503, new CCLIReporting.Song("190579", false) },
                { 603, new CCLIReporting.Song("3234813", false) },
                { 617, new CCLIReporting.Song("17597", false) },
                { 637, new CCLIReporting.Song("1045238", false) },
                { 646, new CCLIReporting.Song("16653", false) },
                { 647, new CCLIReporting.Song("3350395", false) },
                { 673, new CCLIReporting.Song("2684543", true) },
                { 703, new CCLIReporting.Song("20285", false) },
                { 717, new CCLIReporting.Song("19605", false) },
                { 731, new CCLIReporting.Song("6012702", false) },
                { 781, new CCLIReporting.Song("3438561", true) },
                { 784, new CCLIReporting.Song("2982812", true) },
                { 801, new CCLIReporting.Song("3238864", true) },
                { 808, new CCLIReporting.Song("1585970", false) },
                { 821, new CCLIReporting.Song("78897", false) },
                { 828, new CCLIReporting.Song("93724", true) },
                { 853, new CCLIReporting.Song("5860591", true) },
                { 855, new CCLIReporting.Song("3143674", true) },
                { 865, new CCLIReporting.Song("31315", true) },
                { 913, new CCLIReporting.Song("1383", false) },
                { 915, new CCLIReporting.Song("4755580", true) },
                { 916, new CCLIReporting.Song("21167", false) },
                { 925, new CCLIReporting.Song("1493", false) },
                { 931, new CCLIReporting.Song("3372098", false) },
                { 939, new CCLIReporting.Song("40801", false) },
                { 942, new CCLIReporting.Song("114452", false) },
            };

            return magicList;
        }
    }
}
