using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Facebook.Web.Models.Facebook
{
    public class FacebookBatchRequest
    {
        public string access_token { get; set; }
        public List<Batch> batch { get; set; }
    }

    public class Batch
    {
        public string method { get; set; }
        public string relative_url { get; set; }
    }
}