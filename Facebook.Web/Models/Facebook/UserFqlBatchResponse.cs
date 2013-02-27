using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Facebook.Web.Models.Facebook
{
    public class UserFqlBatchResponse
    {
        public List<UserFqlBatchResponseData> data { get; set; }
    }

    public class UserFqlBatchResponseData
    {
        public string name { get; set; }
        public List<FqlUser> fql_result_set { get; set; }
    }
}