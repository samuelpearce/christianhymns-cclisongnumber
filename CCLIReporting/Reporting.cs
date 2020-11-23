using System;
using System.Net;
using System.Net.Http;
using Flurl;
using Flurl.Http;

namespace CCLIReporting
{
    public class Reporting
    {
        private readonly String jwtWebToken;

        private readonly IFlurlRequest req;

        public Reporting(String token)
        {
            this.jwtWebToken = token;
        }

        public Search Search(String searchTerm)
        {
            return new Search(CloneRequest(), searchTerm);
        }
        public Search Search(String searchTerm, int page)
        {
            return new Search(CloneRequest(), searchTerm, page);
        }

        private IFlurlRequest CloneRequest()
        {
            var baseUrl = new Url("https://reporting.ccli.com/api/").Clone();
            return baseUrl.WithCookie("CCLI_JWT_AUTH", jwtWebToken);
        }
    }
}
