using System;

namespace CCLIReporting
{
    public class ReportingSearchPage
    {
        public Int32? page;
        public Int32? totalPages;
        public Int32? totalCount;
        public String searchTerm;
        public Results results;
    }
}