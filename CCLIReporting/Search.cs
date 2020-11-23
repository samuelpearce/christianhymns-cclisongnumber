using Flurl;
using Flurl.Http;
using System;

namespace CCLIReporting
{
    public class Search
    {
        public readonly IFlurlRequest req;
        public readonly string searchTerm;
        public readonly int page;
        public readonly string[] searchCategory;
        public readonly string[] searchFilter;

        public Search(IFlurlRequest req, string searchTerm) : this(req, searchTerm, 1, new string[0] { }, new string[0] { }) { }
        public Search(IFlurlRequest req, string searchTerm, int page) : this(req, searchTerm, page, new string[0] { }, new string[0] { }) { }


        public Search(IFlurlRequest req, string searchTerm, int page, string[] searchCategory, string[] searchFilter)
        {
            this.req = req;
            this.searchTerm = searchTerm;
            this.page = page;
            this.searchCategory = searchCategory;
            this.searchFilter = searchFilter;
            req
                .AppendPathSegment("search")
                .SetQueryParam("searchTerm", searchTerm)
                .SetQueryParam("searchCategory", "all")
                .SetQueryParam("searchFilters", "[]")
                .SetQueryParam("page", page);
        }

        public IFlurlRequest Request()
        {
            return req;
        }

        public ReportingSearchPage SearchAsync()
        {
            var res = req.GetJsonAsync<ReportingSearchPage>();
            res.Wait();
            return res.Result;
        }
    }
}