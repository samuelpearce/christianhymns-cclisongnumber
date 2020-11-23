using Microsoft.VisualStudio.TestTools.UnitTesting;
using Flurl.Http.Testing;

namespace CCLIReporting.Tests
{
    [TestClass()]
    public class SearchTests
    {
        [TestMethod()]
        public void SearchAsyncTest()
        {
            using (var httpTest = new HttpTest())
            {
                var r = new CCLIReporting.Reporting("token");
                var result = r.Search("SearchString").SearchAsync();
                httpTest.ShouldHaveCalled("https://reporting.ccli.com/api/search?searchTerm=SearchString&searchCategory=all&searchFilters=%5B%5D&page=1");
            }
        }
    }
}