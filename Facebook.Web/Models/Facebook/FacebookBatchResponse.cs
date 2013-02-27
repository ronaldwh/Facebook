using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Facebook.Web.Models.Facebook
{
    public class FacebookBatchResponse
    {
        public string code { get; set; }
        public List<Header> headers { get; set; }
        public string body { get; set; }
    }

    public class Header
    {
        public string name { get; set; }
        public string value { get; set; }
    }
}