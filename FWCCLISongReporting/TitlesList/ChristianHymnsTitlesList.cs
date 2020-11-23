using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using System.Text;

namespace ChristianHymns.TitlesList
{
    public class ChristianHymnsTitlesList
    {
        private string path;

        public ChristianHymnsTitlesList(string pathToCsv)
        {
            this.path = pathToCsv;
        }

        public Dictionary<string, Song> Load()
        {
            Dictionary<string, Song> hymns = new Dictionary<string, Song>();

            using (TextFieldParser csvParser = new TextFieldParser(this.path, Encoding.UTF8))
            {
                csvParser.SetDelimiters("\t");
                while (!csvParser.EndOfData)
                {
                    string[] fields = csvParser.ReadFields();
                    int num = 0;
                    var song = new Song(
                        int.Parse(fields[num]),
                        fields[num += 1],
                        fields[num += 2],
                        fields[num += 1]);
                    hymns.Add(song.id.ToString(), song);
                }
            }
            return hymns;
        }
    
    }
}
