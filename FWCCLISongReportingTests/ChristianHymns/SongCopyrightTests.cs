using Microsoft.VisualStudio.TestTools.UnitTesting;
using FWCCLISongReporting.ChristianHymns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWCCLISongReporting.ChristianHymns.Tests
{
    [TestClass()]
    public class SongCopyrightTests
    {
        [TestMethod()]
        public void SongAuthorsTestSimple()
        {
            var copyright = new SongCopyright("William Kethe d. 1594");

            Assert.AreEqual("William Kethe", copyright.SongContributor().First().Name());
            Assert.IsTrue(copyright.SongContributor().Count == 1);

        }

        [TestMethod()]
        public void SongAuthorsTestManyAuthors()
        {
            // Original testcase - L instead of 1
            // Nicolaus Ludwig von Zinzendorf, 1700-60, v. l
            String many = "Nicolaus Ludwig von Zinzendorf, 1700-60, v. 1 ; Johann Nitschmann, 1712-83, vv. 2-4; Anna Nitschmann, 1715-60, v. 5; tr. by John Wesley, 1703-91";
            var copyright = new SongCopyright(many);
            
            Assert.AreEqual("1760", copyright.SongContributor()[0].Death());
            Assert.AreEqual("Johann", copyright.SongContributor()[1].FirstName());
            Assert.AreEqual("1715", copyright.SongContributor()[2].Born());
            Assert.AreEqual(ContributionType.Translator, copyright.SongContributor()[3].ContributionType());

        }

        [TestMethod()]
        public void SongCopyrightTest()
        {
            var copyright = new SongCopyright("David G Preston, b. 1939 © Author / Jubilate Hymns");

            Assert.AreEqual("© Author / Jubilate Hymns", copyright.Copyright());
        }

        [TestMethod()]
        public void MultipleAuthors()
        {
            var copyright = new SongCopyright("Isaac Watts 1674-1748; altd. by John Wesley 1703-91;");
            Assert.AreEqual("Isaac", copyright.SongContributor().First().FirstName());
            Assert.AreEqual("Watts", copyright.SongContributor().First().LastName());
            Assert.AreEqual("John", copyright.SongContributor().Skip(1).First().FirstName());
            Assert.AreEqual("Wesley", copyright.SongContributor().Skip(1).First().LastName());
        }

        [TestMethod()]
        public void SongCopyrightFromBookTest()
        {
            var Book = new SongCopyright("SCOTTISH PSALTER*, 1650");

            Assert.AreEqual(ContributionType.Book, Book.SongContributor().First().ContributionType());
            Assert.IsTrue(Book.isPublicDomain());
            var Normal = new SongCopyright("Michael Perry, 1942-96 © Mrs B Perry / Jubilate Hymns");

            Assert.AreNotEqual(ContributionType.Book, Normal.SongContributor().First().ContributionType());
            Assert.IsFalse(Normal.isPublicDomain());
        }
        
        [TestMethod()]
        public void WithOthersTest()
        {
            var copyright = new SongCopyright("Joachim Neander, 1650-80; tr. by Catherine Winkworth, 1827-78, and others");
            Assert.AreEqual("Joachim", copyright.SongContributor().First().FirstName());
            Assert.AreEqual("Neander", copyright.SongContributor().First().LastName());
            Assert.AreEqual("Catherine", copyright.SongContributor().Skip(1).First().FirstName());
            Assert.AreEqual("Winkworth", copyright.SongContributor().Skip(1).First().LastName());
        }
    }
}