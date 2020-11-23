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
    public class SongContributorTests
    {
        [TestMethod()]
        public void ValidNameWithCopyrightTest()
        {
            var test = new SongContributor("Leith Samuel, 1915-99 © Mrs E Samuel");
            var asdf = test.FirstName();
            Assert.AreEqual("Leith", test.FirstName());
            Assert.AreEqual("", test.MiddleNamesOrParts());
            Assert.AreEqual("Samuel", test.LastName());
            Assert.AreEqual("1915", test.Born());
            Assert.AreEqual("1999", test.Death());
            Assert.AreEqual(false, test.IsAlive());
            Assert.AreEqual("© Mrs E Samuel", test.Copyright());
        }

        public void ValidNameNoCommaTest()
        {
            var test = new SongContributor("Isaac Watts 1674 - 1748");
            var asdf = test.FirstName();
            Assert.AreEqual("Isaac", test.FirstName());
            Assert.AreEqual("", test.MiddleNamesOrParts());
            Assert.AreEqual("Watts", test.LastName());
            Assert.AreEqual("1674", test.Born());
            Assert.AreEqual("1748", test.Death());
            Assert.AreEqual(false, test.IsAlive());
            Assert.AreEqual("", test.Copyright());            
        }

        [TestMethod()]
        public void ValidNameWithLongerCopyrightTest()
        {
            var test = new SongContributor("George Wallace Briggs, 1875-1959 © 1953, Renewed 1981 The Hymn Society/ Hope Publishing Company/ CopyCare");
            var asdf = test.FirstName();
            Assert.AreEqual("George", test.FirstName());
            Assert.AreEqual("Wallace", test.MiddleNamesOrParts());
            Assert.AreEqual("Briggs", test.LastName());
            Assert.AreEqual("1875", test.Born());
            Assert.AreEqual("1959", test.Death());
            Assert.AreEqual(false, test.IsAlive());
            Assert.AreEqual("© 1953, Renewed 1981 The Hymn Society/ Hope Publishing Company/ CopyCare", test.Copyright());
        }

        [TestMethod()]
        public void ValidNameAliveTest()
        {
            var test = new SongContributor("Richard Bewes, b. 1934 © Author/Jubilate Hymns");
            var asdf = test.FirstName();
            Assert.AreEqual("Richard", test.FirstName());
            Assert.AreEqual("", test.MiddleNamesOrParts());
            Assert.AreEqual("Bewes", test.LastName());
            Assert.AreEqual("1934", test.Born());
            Assert.AreEqual("", test.Death());
            Assert.AreEqual(true, test.IsAlive());
            Assert.AreEqual(ContributionType.Author, test.ContributionType());
            Assert.AreEqual("© Author/Jubilate Hymns", test.Copyright());
        }

        [TestMethod()]
        public void ValidName3DigitDateTest()
        {
            var test = new SongContributor("Ambrose, c. 339-97");
            var asdf = test.FirstName();
            Assert.AreEqual("", test.FirstName());
            Assert.AreEqual("", test.MiddleNamesOrParts());
            Assert.AreEqual("Ambrose", test.LastName());
            Assert.AreEqual("339", test.Born());
            Assert.AreEqual("397", test.Death());
            Assert.AreEqual(false, test.IsAlive());
            Assert.AreEqual("", test.Copyright());
        }

        [TestMethod()]
        public void ValidNameWithVersesTest()
        {
            var test = new SongContributor("Nicolaus Ludwig von Zinzendorf, 1700-60, v. 1");
            var asdf = test.FirstName();
            Assert.AreEqual("Nicolaus", test.FirstName());
            Assert.AreEqual("Ludwig von", test.MiddleNamesOrParts());
            Assert.AreEqual("Zinzendorf", test.LastName());
            Assert.AreEqual("1700", test.Born());
            Assert.AreEqual("1760", test.Death());
            Assert.AreEqual(false, test.IsAlive());
            Assert.AreEqual("", test.Copyright());
        }

        [TestMethod()]
        public void Translator()
        {
            var withTranslation = new SongContributor("tr. by John Mason Neale, 1818-66");
            Assert.AreEqual("John", withTranslation.FirstName());
            Assert.AreEqual("Mason", withTranslation.MiddleNamesOrParts());
            Assert.AreEqual("Neale", withTranslation.LastName());
            Assert.AreEqual("1818", withTranslation.Born());
            Assert.AreEqual("1866", withTranslation.Death());
            Assert.AreEqual(false, withTranslation.IsAlive());
            Assert.AreEqual("", withTranslation.Copyright());
            Assert.AreEqual(ContributionType.Translator, withTranslation.ContributionType());
        }

    }
}