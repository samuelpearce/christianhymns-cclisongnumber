using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCLIReporting;
using System.IO;
using Newtonsoft.Json;

namespace ChristianHymnsCCLISongNumber.Search
{
    class CachedReporting
    {
        private readonly Reporting search;

        public CachedReporting(Reporting search)
        {
            this.search = search;
        }

        public ReportingSearchPage Search(string searchTerms)
        {
            var tmpPath = Path.GetTempPath() + "ccli\\";
            Directory.CreateDirectory(tmpPath);
            tmpPath += MD5(searchTerms);
            if (File.Exists(tmpPath))
            {
                return JsonConvert.DeserializeObject<ReportingSearchPage>(File.ReadAllText(tmpPath));
            }
            else
            {
                // Super lazy hack
                // Need to get > 1 page so I patch it in here...
                int? count = 20;
                int i = 1;
                ReportingSearchPage results = null;
                List<Song> songsList = new List<Song>();
                while (count >= 10 && songsList.Count() < 40)
                {
                    results = this.search.Search(searchTerms, i).SearchAsync();
                    count = results.results.songs.Count();
                    songsList.AddRange(results.results.songs);
                    i += 1;
                }
                // Lazy hack - adhere to interface by adding all song results to last request
                results.results.songs.RemoveAll(m => true);
                results.results.songs.AddRange(songsList);
                File.WriteAllText(tmpPath, JsonConvert.SerializeObject(results));
                return results;
            }

        }

        private String MD5(string s)
        {
            using (var provider = System.Security.Cryptography.MD5.Create())
            {
                StringBuilder builder = new StringBuilder();

                foreach (byte b in provider.ComputeHash(Encoding.UTF8.GetBytes(s)))
                    builder.Append(b.ToString("x2").ToLower());

                return builder.ToString();
            }
        }
    }
}
