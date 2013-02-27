using Facebook.Web.Models.Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Facebook.Web.Models
{
    public class HomeIndexFqlModel
    {
        public FqlUser user { get; set; }
        public FqlUser significant_other { get; set; }
        public List<FqlUser> friends { get; set; }

        public HomeIndexFqlModel()
        {
            this.user = new FqlUser();
            this.friends = new List<FqlUser>();
        }
    }
}