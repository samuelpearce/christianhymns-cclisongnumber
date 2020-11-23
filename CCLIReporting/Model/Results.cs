using System.Collections.Generic;

namespace CCLIReporting
{
    public class Results
    {
        private List<Song> _songs;
        public List<Song> songs
        {
            get
            {
                if (_songs == null)
                {
                    songs = new List<Song>();
                }
                return _songs;
            }
            set
            {
                _songs = value;
            }
        }
    }
}