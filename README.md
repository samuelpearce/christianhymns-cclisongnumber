# Christian Hymns to CCLI Song Number

Scripts/tools to create a file containing [Christan Hymns](https://www.christianhymns.org.uk/) Song numbers and [CCLI](https://ccli.com) Song Numbers for licensing reporting purposes.

# Quick Start

* Project built using Visual Studio 2017 (Windows)
* Requires Chrome Selenium from NuGet (probably breaking any Cross-platform support)
* Main.cs is located at `ChristianHymnsCCLISongNumber/Program.cs`

# Overview
Task: Identify the CCLI song numbers for the hymns listed in Christan Hymns. Produce a CSV of Christan Hymns Song Numbers and CCLI Song Numbers

1) How do you get hymns titles and numbers from Christan Hymns
2) How do we match these up with the songs listed in the CCLI Reporting Tool

Matching accuracy
* Accuracy will be gained by comparing the title from CH and CCLI
* Further accuracy will be checked by matching authors
* A potential match must also match the licence. e.g. if a song in the public domain, CCLI must also report the song in the public domain

To the best of my knowledge, under UK copyright law, song lyrics enter the public domain after 70 years of the death of the author.

We consider a song to be in the public domain 
* where the death of the author is <= 1948, or,
* if the death is unknown, born at least 200 years ago. 
* The author is anonymous or unknown

# (TL;DR) Download CSV/TSV

Download the output CSV at [/releases/latest](/releases/latest)

Suggested usage
* Quick lookup of CCLI Song Numbers
* Programmatic use in any online lyric management/reporting tools

# Process

## 1. Get Christan Hymn song titles?
A friend found a way to do this, and the results are [here](/ChristianHymnsCCLISongNumber/resources/SongList.txt).

## 2. How to search for CCLI songs via code?
Before November 2020, Selenium was used to scrape/interact with the reporting website. 

After November 2020, the site was upgraded to a React/Rest API system. Selenium is still required as Json Web Tokens are embedded in a cookie. As of Nov 2020, it doesn't seem possible to authenticate without using a full browser as Google ReCaptcha is used. [Automated Login via Selenium](/CCLIReporting/Login/WebDriver.cs). To avoid using Selenium for the full API requests, reading HTTP-only cookies is required. Once I have the Token, I can close Chrome and use a standard HTTP web request library.

# Challenges

## Challenges with Christian Hymns data

* Christan Hymn Song titles are not always the same as the reporting tools. 
The title in Christan Hymns for song #1 is `The worship on God` (the title of the section) but the title in CCLI is `All People That On Earth Do Dwell` (the first line of the hymn)
* Christan Hymns sometimes modernises words (thee > you/hath > has) matching title matches fail
* Author and copyright lines are not consistent
Example author and copyright
```
William Kethe d. 1594
Nicolaus Ludwig von Zinzendorf, 1700-60, v. 1 ; Johann Nitschmann, 1712-83, vv. 2-4; Anna Nitschmann, 1715-60, v. 5; tr. by John Wesley, 1703-91
David G Preston, b. 1939 © Author / Jubilate Hymns
Isaac Watts 1674-1748; altd. by John Wesley 1703-91;
SING PSALMS, 2003 © Free Church of Scotland
Authors John Mason	John Mason, c. 1646-94
Michael Perry, 1942-96 © Mrs B Perry / Jubilate Hymns
SCOTTISH PSALTER*, 1650
Samuel Crossman, 1624-83
C A Tydeman
Joachim Neander, 1650-80; tr. by Catherine Winkworth, 1827-78, and others
```

and my favourite: `Nicolaus Ludwig von Zinzendorf, 1700-60, v. l`. Yes, that is a lower case L.


For fun, here is the regular expression
``` 
^([A-Za-z*\- ]+),?( [bdc.]+\.)?( [0-9]{0,4})?(-([0-9]{0,4}))?(, vv?. [0-9]-?[0-9]?)?( © [\w\d&©,. /]+)?$
```
That surprisingly works for most copyright strings. For everything else, I string-match it as it's not worth trying to parse them. [SongContributor.cs](/FWCCLISongReporting/ChristianHymns/SongContributor.cs). The format is so random, I don't think it's worth making it better.

## Challenges with CCLI data
Most of the challenges was parsing the HTML but the new API makes parsing JSON easy (good work CCLI!)

However, the data has one problem (although this is a gripe with the music license holders). I have the same problem with YouTube. 

-- Rant
_Basically, a publisher gets public domain lyrics, prints them, but registers the lyrics as their own (I'm not talking a translation, or modification, but a one-to-one copy). The publisher now gets to collect royalties on content they didn't produce. 
I have the same problem with YouTube. Music Publishers make composition claims for songs like 'Amazing Grace' where the words and music were written over 200 years ago. If your church musically performs and sings it (no pre-recorded music used) and it is live-streamed/recorded, you risk being taken down by the music industry. Thankfully it didn't happen to us in the UK, although anyone watching in Denmark was blocked._
-- end rant

TL;DR: Programatically detecting hymns by title alone often matches public domain songs with 'registered' licenced songs. Unless otherwise stated in the copyright line, Christan Hymns have not modified, or not claiming copyright on the modified/modernised changes to Public Domain hymns. (Thumbs up Christian Hymns!)
Technically, if the song is in the public domain, you don't need to register it, but I like data completeness and sometimes the words are out of copyright but the music is not (so still needs to be reported)
I have no problem with paying, but I do have a problem when people are collecting money for something they don't have the right to. This isn't a rant on CCLI, they are just doing what the copyright holders tell them. Ok, rant really is over.


Well done, you made it this far!
