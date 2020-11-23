using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace FWCCLISongReporting.ChristianHymns.Tests
{
    [TestClass()]
    public class SongCopyrightTests
    {
        [TestMethod()]
        public void SongAuthorsTestSimple()
        {
            var copyright = new SongCopyright("William Kethe d. 1594");

            Assert.AreEqual("William Kethe", copyright.SongContributors().First().Name());
            Assert.IsTrue(copyright.SongContributors().Count == 1);
        }

        [TestMethod()]
        public void SongAuthorsTestManyAuthors()
        {
            // Original testcase - L instead of 1
            // Nicolaus Ludwig von Zinzendorf, 1700-60, v. l
            String many = "Nicolaus Ludwig von Zinzendorf, 1700-60, v. 1 ; Johann Nitschmann, 1712-83, vv. 2-4; Anna Nitschmann, 1715-60, v. 5; tr. by John Wesley, 1703-91";
            var copyright = new SongCopyright(many);
            
            Assert.AreEqual("1760", copyright.SongContributors()[0].Death());
            Assert.AreEqual("Johann", copyright.SongContributors()[1].FirstName());
            Assert.AreEqual("1715", copyright.SongContributors()[2].Born());
            Assert.AreEqual(ContributionType.Translator, copyright.SongContributors()[3].ContributionType());

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
            Assert.AreEqual("Isaac", copyright.SongContributors().First().FirstName());
            Assert.AreEqual("Watts", copyright.SongContributors().First().LastName());
            Assert.AreEqual("John", copyright.SongContributors().Skip(1).First().FirstName());
            Assert.AreEqual("Wesley", copyright.SongContributors().Skip(1).First().LastName());
        }

        [TestMethod()]
        public void SongCopyrightFromBookTest()
        {
            var Book = new SongCopyright("SCOTTISH PSALTER*, 1650");

            Assert.AreEqual(ContributionType.Book, Book.SongContributors().First().ContributionType());
            Assert.IsTrue(Book.isPublicDomain());
            var Normal = new SongCopyright("Michael Perry, 1942-96 © Mrs B Perry / Jubilate Hymns");

            Assert.AreNotEqual(ContributionType.Book, Normal.SongContributors().First().ContributionType());
            Assert.IsFalse(Normal.isPublicDomain());
        }
        
        [TestMethod()]
        public void WithOthersTest()
        {
            var copyright = new SongCopyright("Joachim Neander, 1650-80; tr. by Catherine Winkworth, 1827-78, and others");
            Assert.AreEqual("Joachim", copyright.SongContributors().First().FirstName());
            Assert.AreEqual("Neander", copyright.SongContributors().First().LastName());
            Assert.AreEqual("Catherine", copyright.SongContributors().Skip(1).First().FirstName());
            Assert.AreEqual("Winkworth", copyright.SongContributors().Skip(1).First().LastName());
        }
        
        public void TwoAuthors()
        {
            var copyright = new SongCopyright("Matthew Bridges, 1800-94 and Godfrey Thring, 1823-1903");
            Assert.AreEqual("Matthew", copyright.SongContributors().First().FirstName());
            Assert.AreEqual("Bridges", copyright.SongContributors().First().LastName());
            Assert.AreEqual("Godfrey", copyright.SongContributors().Skip(1).First().FirstName());
            Assert.AreEqual("Thring", copyright.SongContributors().Skip(1).First().LastName());
        }
    }
}