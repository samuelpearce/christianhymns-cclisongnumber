﻿using ChristianHymns.TitlesList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CCLIReporting;
using ChristianHymnsCCLISongNumber.Search;
using System.Text.RegularExpressions;
using FWCCLISongReporting.ChristianHymns;
using System.Text;

namespace ChristianHymnsCCLISongNumber
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Obtaining login token");
            String token = "";
            
            using (var webDriver = new WebDriver(ccliusername, cclipassword))
            {
                token = webDriver.jwt_token;
            }
            

            Console.WriteLine("Got Token: " + token);
            Console.WriteLine("-------------------");
            Console.WriteLine("-------------------");

            var reporting = new CachedReporting(new Reporting(token));

            OutputCcliIdFile outputCcliIdFile = new OutputCcliIdFile();

            String tmpFile = null;
            Dictionary<string, ChristianHymns.TitlesList.Song> christianHymnsSongsList;
            try
            {
                tmpFile = Path.GetTempFileName();
                File.WriteAllText(tmpFile, Properties.Resources.SongList);
                var songNumberFile = new ChristianHymnsTitlesList(tmpFile);
                christianHymnsSongsList = songNumberFile.Load();
            }
            finally
            {
                File.Delete(tmpFile);
            }

            foreach (KeyValuePair<string, ChristianHymns.TitlesList.Song> songPair in christianHymnsSongsList)
            {
                String id = songPair.Key;
                int idAsInt = Int32.Parse(id);
                ChristianHymns.TitlesList.Song song = songPair.Value;

                if (idAsInt == 146)
                {
                    String aseedf = "";
                }

                string ccliId = null;
                CCLIReporting.Song chosenSong = null;

                var magicMatchList = HymnMatches.load();

                chosenSong = new CCLIReporting.Song();
                chosenSong.publicDomain = song.author.isPublicDomain();
                String searchTerm = "";
                CCLIReporting.Song magicSong = null;
                var skipValidation = false;
                if (magicMatchList.ContainsKey(idAsInt))
                {
                    magicSong = magicMatchList.First(s => s.Key == idAsInt).Value;
                    searchTerm = magicSong.ccliSongNo;
                    skipValidation = true;
                }
                else
                {
                    searchTerm = "\"" + song.title + "\" " + String.Join(" ", song.author.SongContributors()
                        .Select(contributor => String.Format("{0} {1}", contributor.FirstName(), contributor.LastName()))
                    );
                    skipValidation = false;
                }
                var results = reporting.Search(searchTerm);

                Console.WriteLine("---------------");
                Console.WriteLine("{0} {1}", id, searchTerm);
                Console.WriteLine("---------------");

                if (skipValidation)
                {
                    chosenSong = results.results.songs[0];
                    ccliId = chosenSong.ccliSongNo;
                }
                else
                {
                    var potentialMatches = new List<CCLIReporting.Song>();
                    foreach (var result in results.results.songs)
                    {
                        int weight = 0;
                        // For titles
                        var normS1 = normalise(song.title);
                        var normS2 = normalise(result.title);
                        if (normS1 == normS2)
                        {
                            weight += 20;
                        }
                        else if (normS1.Contains(normS2))
                        {
                            weight += 10;
                        }
                        else if (normS2.Contains(normS1))
                        {
                            weight += 5;
                        }

                        // For Authors
                        //var authorS1 = normalise(String.Join(" ", song.author.SongContributors().Select(contributor => String.Format("{0} {1}", contributor.FirstName(), contributor.LastName()))));
                        var originalAuthorsList = normaliseAuthors(song.author.SongContributors());
                        var originalAuthorsComparible = normalise(String.Join("", originalAuthorsList));
                        var remote = new List<SongContributor>();
                        foreach (var remoteAuthor in result.authors)
                        {
                            remote.Add(new SongContributor(remoteAuthor));
                            // remote.Add(remoteSongContributor.FirstName());
                            //remote.Add(remoteSongContributor.LastName());
                        }
                        var remoteAuthorsList = normaliseAuthors(remote);
                        var remoteAuthorsComparible = normalise(String.Join("", remoteAuthorsList));
                        if (originalAuthorsComparible == remoteAuthorsComparible)
                        {
                            weight = weight * 3;
                        }

                        if (Math.Abs(originalAuthorsList.Count() - remoteAuthorsList.Count()) <= 1)
                        {
                            weight = weight * 3;
                        }
                    
                        if (weight >= 40)
                        {
                            potentialMatches.Add(result);
                        }
                    }
                    
                    var cachedIsPublicDomain = song.author.isPublicDomain();

                    foreach (var match in potentialMatches)
                    {
                        // If song is not PD, select first exact match found (until we can distingush better)
                        // If song is PD, then ensure ensure the match is also PD

                        if (match.publicDomain == cachedIsPublicDomain)
                        {
                            chosenSong = match;
                            ccliId = chosenSong.ccliSongNo;
                            break;
                        }
                    }
                }

                var publicDomain = (magicSong == null) ? chosenSong.publicDomain : magicSong.publicDomain;

                outputCcliIdFile.Output(song.id.ToString(), ccliId, publicDomain, song.title, song.author.ToString(), song.metre);
            }

        }

        private static List<String> normaliseAuthors(IList<SongContributor> SongContributors)
        {
            List<string> list = new List<string>();
            foreach (var item in SongContributors)
            {
                var tmpName = item.ToString()
                    .Replace("Authors\r\n", "")
                    .Replace("Rev. ", "");

                var contributor = new SongContributor(tmpName);
                list.Add(contributor.FirstName());
                list.Add(contributor.LastName());
            }
            list.RemoveAll(x => x == "");
            list.Sort();
            return list;
        }

        private static bool normaliseAndMatchesOrOneContainsTheOther(string s1, string s2)
        {
            var normS1 = normalise(s1);
            var normS2 = normalise(s2);
            return normS1 == normS2
                || normS1.Contains(normS2)
                || normS2.Contains(normS1);
        }

        private static List<String> wordReplacementList = new List<String>() {
            "thy",
            "your",
            "you",
            "thee",
            "thou",
            "has",
            "hath",
        };

        private static string normalise(string s)
        {
            Regex rgx = new Regex("[^a-zA-Z ]");    
            s = rgx.Replace(s.Trim().ToLower(), "");
            foreach (var l in wordReplacementList)
            {
                s = s.Replace(l, "");
            }
            return s;
        }

        
    }
}