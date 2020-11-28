using System.Collections.Generic;

namespace CCLIReporting
{
    public class Song
    {
        public string id { get; set; }
        public string title { get; set; }
        public object lastReported { get; set; }
        public List<string> authors { get; set; }
        public string ccliSongNo { get; set; }
        public bool publicDomain { get; set; } = false;
        public string playbackUrl { get; set; }
        public bool popular { get; set; }

        public Song() { }

        public Song(string ccliSongNumber, bool publicDomain)
        {
            this.ccliSongNo = ccliSongNumber;
            this.publicDomain = publicDomain;
        }
    }
}