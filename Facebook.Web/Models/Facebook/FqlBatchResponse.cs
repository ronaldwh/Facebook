using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Facebook.Web.Models.Facebook
{
    public class FqlBatchResponse
    {
        public List<FqlBatchResponseData> data { get; set; }
    }

    public class FqlBatchResponseData
    {
        public string name { get; set; }
        public string fql_result_set { get; set; }
    }
}