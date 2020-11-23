using FWCCLISongReporting.ChristianHymns;

namespace ChristianHymns.TitlesList
{
    public class Song
    {
        public int id;
        public string title;
        public SongCopyright author;
        public string metre;

        public Song(int id, string title, string author, string metre)
        {
            this.id = id;
            this.title = title;
            this.author = new SongCopyright(author);
            this.metre = metre;
        }
    }
}